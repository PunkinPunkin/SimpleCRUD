using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Shared.Attribute
{
    public sealed class TaiwanIdentityCardNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string id = value.ToString();
                if (!CheckForIdentityCardNumber(id))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }

        /// <summary>
        /// 檢查身分證字號 Addb by Charly 2012/4/19
        /// </summary>
        bool CheckForIdentityCardNumber(string inputValue)
        {
            int[] uid = new int[10];    //數字陣列存放身分證字號用
            int chkTotal;               //計算總和用
            Regex reg1 = new Regex(@"^[A-Za-z]+$");

            if (inputValue.Length == 10)    //檢查長度
            {
                if (reg1.IsMatch(inputValue.Substring(1, 1))) //檢查第二碼是否為英文
                {
                    return false;
                }

                //if (inputValue.Substring(1, 1) != "1" || inputValue.Substring(1, 1) != "2")
                //    return false;


                inputValue = inputValue.ToUpper();    //將身分證字號英文改為大寫

                //將輸入的值存入陣列中
                for (int i = 1; i < inputValue.Length; i++)
                {
                    uid[i] = Convert.ToInt32(inputValue.Substring(i, 1));
                }
                //將開頭字母轉換為對應的數值
                switch (inputValue.Substring(0, 1).ToUpper())
                {
                    case "A": uid[0] = 10; break;
                    case "B": uid[0] = 11; break;
                    case "C": uid[0] = 12; break;
                    case "D": uid[0] = 13; break;
                    case "E": uid[0] = 14; break;
                    case "F": uid[0] = 15; break;
                    case "G": uid[0] = 16; break;
                    case "H": uid[0] = 17; break;
                    case "I": uid[0] = 34; break;
                    case "J": uid[0] = 18; break;
                    case "K": uid[0] = 19; break;
                    case "L": uid[0] = 20; break;
                    case "M": uid[0] = 21; break;
                    case "N": uid[0] = 22; break;
                    case "O": uid[0] = 35; break;
                    case "P": uid[0] = 23; break;
                    case "Q": uid[0] = 24; break;
                    case "R": uid[0] = 25; break;
                    case "S": uid[0] = 26; break;
                    case "T": uid[0] = 27; break;
                    case "U": uid[0] = 28; break;
                    case "V": uid[0] = 29; break;
                    case "W": uid[0] = 32; break;
                    case "X": uid[0] = 30; break;
                    case "Y": uid[0] = 31; break;
                    case "Z": uid[0] = 33; break;
                }
                //檢查第一個數值是否為1.2(判斷性別)
                if (uid[1] == 1 || uid[1] == 2)
                {
                    chkTotal = (uid[0] / 10 * 1) + (uid[0] % 10 * 9);

                    int k = 8;
                    for (int j = 1; j < 9; j++)
                    {
                        chkTotal += uid[j] * k;
                        k--;
                    }

                    chkTotal += uid[9];

                    if (chkTotal % 10 != 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
