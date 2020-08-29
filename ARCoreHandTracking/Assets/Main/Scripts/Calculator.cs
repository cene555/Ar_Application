using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Calculator : MonoBehaviour
{
    public Text t;
    void Update()
    {
        Calculate();
    }
    void Calculate()
    {
        bool can = false;
        if (t.text.Length > 0)
        {
            
            foreach (char i in t.text)
            {
                /*
                if (i == '+' | i == '-' | i == '*' | i == '/')
                {
                    k++;
                }
                */
                if (i == '=')
                {
                    can = true;
                    break;
                }

            }
        }
            if (can)
            {
                List<double> numbers = new List<double>();
                List<char> arr = new List<char>();
                string number = "";
                int n = 0;
                foreach (char i in t.text)
                {
                    if (i == '+' | i == '-' | i == '*' | i == '/')
                    {
                        
                        numbers.Add(Convert.ToDouble(number));
                        arr.Add(i);
                        number = "";
                    }
                    else
                    {
                        number = number + i;
                    }
                    n++;
                    if (n == t.text.Length - 1)
                    {
                        try
                        {
                            numbers.Add(Convert.ToDouble(number));
                        }
                        catch { }
                    }
                }

            double l = numbers[0];
            try
            {
                for (int i1 = 0; i1 < arr.Count; i1++)
                {
                    if (arr[i1] == '+')
                    {
                        l += numbers[i1 + 1];
                    }
                    if (arr[i1] == '-')
                    {
                        l -= numbers[i1 + 1];
                    }
                    if (arr[i1] == '*')
                    {
                        l *= numbers[i1 + 1];
                    }
                    if (arr[i1] == '/')
                    {
                        l /= numbers[i1 + 1];
                    }
                }
                t.text = "";
                t.text = Convert.ToString(l);
                can = false;
            }
            catch
            {
                t.text = "";
                foreach (double i2 in numbers)
                {
                    t.text = t.text + Convert.ToString(i2);
                }
            }

            }
        }
}
