// #define RADIAC_DEBUG


using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace RadiacUI
{
    [RequireComponent(typeof(Text))]
    [RequireComponent(typeof(RadiacDisplayController))]
    public class RadiacTextInput : RadiacInputReceiver
    {
        Text text { get { return this.gameObject.GetComponent<Text>(); } }
        RectTransform rectTransform { get { return this.gameObject.GetComponent<RectTransform>(); } }
        TextGenerator gen { get { return text.cachedTextGenerator; } }
        
        int totalLength { get { return text.text.Length; } }
        
        [SerializeField] Material caretMaterial;
        [SerializeField] Image caret;
        [SerializeField] float caretWidth;
        [SerializeField] AnimationCurve caretAlphaCurve;
        [SerializeField] float caretBlinkSpeedMult = 1.0f;
        
        [SerializeField] bool shift;
        [SerializeField] bool ctrl;
        [SerializeField] bool alt;
        
        /// <summary>
        /// Caret position of the whole string.
        /// </summary>
        [SerializeField] int caretPos;
        
        int caretLine
        {
            get
            {
                for(int i=0; i < gen.lineCount - 1; i++)
                {
                    if(gen.lines[i].startCharIdx <= caretPos && caretPos < gen.lines[i+1].startCharIdx) return i;
                }
                return gen.lineCount - 1;
            }
        }
        
        int caretOffset { get { return caretPos - gen.lines[caretLine].startCharIdx; } }
        
        protected override void Start()
        {
            base.Start();
        }
        
        public override void ReceiveOperator(KeyCode op)
        {
            switch(op)
            {
                case KeyCode.Backspace:
                {
                    // TODO:
                    // Word remove.
                    // Needs word segmentation.
                    
                    if(totalLength != 0)
                    {
                        if(caretPos != 0)
                        {
                            caretPos--;
                        }
                        text.text = text.text.Remove(caretPos, 1);
                    }
                    break;
                }
                
                case KeyCode.LeftArrow:
                {
                    caretPos = Mathf.Max(0, caretPos - 1);
                    break;
                }
                
                case KeyCode.RightArrow:
                {
                    caretPos = Mathf.Min(totalLength, caretPos + 1);
                    break;
                }
                
                case KeyCode.UpArrow:
                {
                    if(caretLine == 0)
                    {
                        caretPos = 0;
                    }
                    else
                    {
                        var indexInLine = caretPos - gen.lines[caretLine].startCharIdx;
                        caretPos = Math.Min(gen.lines[caretLine-1].startCharIdx + indexInLine, gen.lines[caretLine].startCharIdx-1);
                    }
                    break;
                }
                
                case KeyCode.DownArrow:
                {
                    if(caretLine == gen.lines.Count - 1)
                    {
                        caretPos = totalLength;
                    }
                    else
                    {
                        var indexInLine = caretPos - gen.lines[caretLine].startCharIdx;
                        if(caretLine+2 < gen.lineCount)
                        {
                            caretPos = Math.Min(gen.lines[caretLine+1].startCharIdx + indexInLine, gen.lines[caretLine+2].startCharIdx - 1);
                        }
                        else
                        {
                            caretPos = Math.Min(gen.lines[caretLine+1].startCharIdx + indexInLine, totalLength);
                        }
                    }
                    break;
                }
                
                case KeyCode.Return:
                {
                    text.text = text.text.Insert(caretPos, "\n");
                    caretPos++;
                    break;
                }
                
                default: break;
            }
        }
        
        public override void ReceiveChar(char c)
        {
            if(RadiacInputController.shift)
                c = char.ToUpper(c);
            else
                c = char.ToLower(c);
            
            // TODO:
            // Optimize.
            // Should be implemented using a specific data structure which implements IList<char>...
            text.text = text.text.Insert(caretPos, c.ToString());
            
            caretPos++;
        }
        
        protected override void Update()
        {
            base.Update();
            DrawCaret();
        }
        
        public void DrawCaret()
        {
            // TODO:
            // Is this really a good place to run this?
            gen.Populate(text.text, text.GetGenerationSettings(rectTransform.rect.size));
            
            
            var rect = GetCaretRect(caretPos, caretLine);
            caret.rectTransform.sizeDelta = rect.size;
            caret.rectTransform.position = new Vector3(rect.center.x, rect.center.y, caret.rectTransform.position.y);
            caret.GetComponent<RadiacDisplayController>().baseColor =
                caret.GetComponent<RadiacDisplayController>().baseColor.SetA(
                    (focused ? 1f : 0f) * caretAlphaCurve.Evaluate(Time.time * caretBlinkSpeedMult));
        }
        
        Rect GetCaretRect(int caretPos, int caretLine)
        {
            // Magic! about how to find the caret position...
            // See source code of UGUI InputField
            // https://bitbucket.org/Unity-Technologies/ui/src/a3f89d5f7d145e4b6fa11cf9f2de768fea2c500f/UnityEngine.UI/UI/Core/InputField.cs?at=2017.3
            // Line *1960* Function *GenerateCaret* for more details.
            
            var curOffset = new Vector2(
                gen.characters[caretPos].cursorPos.x,
                gen.lines[caretLine].topY) / text.pixelsPerUnit;
            float height = gen.lines[caretLine].height / text.pixelsPerUnit;
            
            var bottomLeft = rectTransform.position + new Vector3(curOffset.x, curOffset.y - height, -1f);
            var topRight = rectTransform.position + new Vector3(curOffset.x + caretWidth, curOffset.y, -1f);
            
            #if RADIAC_DEBUG
            
            var bottomRight = rectTransform.position + new Vector3(curOffset.x + caretWidth, curOffset.y - height, -1f);
            var topLeft = rectTransform.position + new Vector3(curOffset.x, curOffset.y, -1f);
            
            Debug.DrawLine(bottomLeft, bottomRight, Color.red);
            Debug.DrawLine(bottomRight, topRight, Color.red);
            Debug.DrawLine(topRight, topLeft, Color.red);
            Debug.DrawLine(topLeft, bottomLeft, Color.red);
            
            #endif
            
            return new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        }
    }
}
