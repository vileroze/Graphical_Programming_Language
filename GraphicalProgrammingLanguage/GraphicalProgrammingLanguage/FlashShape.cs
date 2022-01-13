using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
    public class FlashShape
    {
        Form1 form;
        ArrayList shapes;
        bool flashFlag = false;
        Thread thread;

        public FlashShape(Form1 form, ArrayList shapes)
        {
            this.form = form;
            this.shapes = shapes;
            thread = new Thread(flashShapes);
            thread.Start();
        }

        /// <summary>
        /// flash alternating color primary and secondary color for each shape
        /// </summary>
        public void flashShapes()
        {
            while (true)
            {
                foreach (Shape shape in shapes)
                {
                    if (flashFlag == false)
                    {
                        if (shape.flash)
                        {
                            shape.fill = true;
                            shape.colour = shape.primaryColor;
                        }
                    }
                    else
                    {
                        if (shape.flash)
                        {
                            shape.fill = true;
                            shape.colour = shape.secondaryColor;
                        }
                        
                    }
                   
                }
                
                flashFlag = !flashFlag; //alternate colors
                form.refreshPictureBox();
                Thread.Sleep(100);
            }
        }
    }
}
