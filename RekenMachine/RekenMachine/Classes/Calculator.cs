using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RekenMachine.Classes
{
    internal class Calculator
    {
        public string TotalString { get; set; }
        private string LeftString;
        private bool LeftNegative = false;

        private string RightString;
        private bool RightNegative = false;

        private bool LeftIsRoot = false;

        private bool LeftHasPower = false;
        private bool LeftPowerNegative = false;
        private string LeftPowerString;
        private bool LeftPowerHasDecimal = false;

        private bool RightIsRoot = false;

        private bool RightHasPower = false;
        private bool RightPowerNegative = false;
        private string RightPowerString;
        private bool RightPowerHasDecimal = false;

        private bool HasDecimalLeft = false;
        private bool HasDecimalRight = false;

        private bool HasOperator = false;

        private bool canCalculate = false;

        private string op;

        public double result;

        public double Memory;
        public Calculator() { }

        public void RecieveInput(string input)
        {
            if (!HasOperator && !LeftHasPower)
            {
                switch (input)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        LeftString += Convert.ToInt32(input);
                        TotalString += input;
                        break;
                    case "0":
                        LeftString += Convert.ToInt32(input);
                        TotalString += input;
                        break;
                    case ",":
                        if (!String.IsNullOrEmpty(TotalString))
                        {
                            if (!HasDecimalLeft)
                            {
                                TotalString += input;
                                LeftString += input;
                                HasDecimalLeft = true;
                            }
                            else
                            {
                                ErrorMsg("This is already a decimal");
                            }
                        }
                        else
                        {
                            ErrorMsg("Cannot add decimal to null");
                        }
                        break;

                    case "*":
                    case "/":
                    case "-":
                    case "+":
                        if (!String.IsNullOrEmpty(TotalString))
                        {
                            TotalString += input;
                            Operator(input);
                        }
                        else
                        {
                            ErrorMsg("Operator cannot go first");
                        }

                        break;

                    case "+-":
                        if (!LeftNegative)
                        {
                            LeftNegative = true;
                            LeftString = "-" + LeftString;
                            TotalString = LeftString;
                        }
                        else
                        {
                            LeftNegative = false;
                            LeftString = LeftString.Remove(0, 1);
                            TotalString = LeftString;
                        }
                        break;

                    case "^":
                        if (!String.IsNullOrEmpty(TotalString))
                        {
                            if (!LeftHasPower)
                            {
                                LeftHasPower = true;
                                TotalString += input;
                                canCalculate = true;
                            }
                            else
                            {
                                LeftHasPower = false;
                                canCalculate = false;
                            }
                        }
                        else
                        {
                            ErrorMsg("Cannot calulate power of null");
                        }
                        break;

                    case "√":
                        if (!LeftIsRoot)
                        {
                            LeftIsRoot = true;
                            LeftString = "√" + LeftString;
                            TotalString = LeftString;
                            canCalculate = true;
                        }
                        else
                        {
                            //LeftIsRoot = false;
                            //LeftString = LeftString.Remove(0, 1);
                            //TotalString = LeftString;
                            //canCalculate = false;
                            ErrorMsg("This is already a root");
                        }
                        break;
                    case "C":
                        Clear();
                        break;
                    case "=":
                        if (canCalculate)
                        {
                            MessageBox.Show(Calculate().ToString());
                        }
                        else
                        {
                            ErrorMsg("Cannot calculate with current input");
                        }
                        break;
                    default:
                        ErrorMsg("Default");
                        break;
                }
            }
            else if (LeftHasPower && !HasOperator)
            {
                switch (input)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        LeftPowerString += input;
                        TotalString += input;
                        break;
                    case "0":
                        LeftPowerString += Convert.ToInt32(input);
                        TotalString += input;
                        break;
                    case ",":
                        if (!String.IsNullOrEmpty(LeftPowerString))
                        {
                            if (!LeftPowerHasDecimal)
                            {
                                LeftPowerString += input;
                                LeftPowerHasDecimal = true;
                                TotalString += input;
                            }
                            else
                            {
                                ErrorMsg("This is already a decimal");
                            }
                        }
                        else
                        {
                            ErrorMsg("Cannot add decimal to null");
                        }
                        break;

                    case "*":
                    case "/":
                    case "-":
                    case "+":
                        if (!String.IsNullOrEmpty(TotalString))
                        {
                            TotalString += input;
                            Operator(input);
                        }
                        else
                        {
                            ErrorMsg("Operator cannot go first");
                        }

                        break;
                    case "+-":
                        if (!LeftPowerNegative)
                        {
                            LeftPowerNegative = true;
                            LeftPowerString = "-" + LeftPowerString;
                            //MessageBox.Show(LeftString + " " + powerString);
                            TotalString = LeftString + "^" + LeftPowerString;
                        }
                        else
                        {
                            LeftPowerNegative = false;
                            LeftPowerString = LeftPowerString.Remove(0, 1);
                            TotalString = LeftString + "^" + LeftPowerString;
                        }
                        break;
                    case "C":
                        Clear();
                        break;
                    case "=":
                        if (canCalculate)
                        {
                            MessageBox.Show(Calculate().ToString());
                        }
                        else
                        {
                            ErrorMsg("Cannot calculate with current input");
                        }
                        break;
                    default:
                        ErrorMsg("Cannot add this to current equation");
                        break;
                }
            }
            else if (HasOperator && !RightHasPower)
            {
                switch (input)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        RightString += input;
                        TotalString += input;
                        canCalculate = true;
                        break;
                    case "0":
                        RightString += input;
                        TotalString += input;
                        break;
                    case ",":
                        if (!String.IsNullOrEmpty(TotalString))
                        {
                            if (!HasDecimalRight)
                            {
                                TotalString += input;
                                RightString += input;
                                HasDecimalRight = true;
                            }
                            else
                            {
                                ErrorMsg("This is already a decimal");
                            }
                        }
                        else
                        {
                            ErrorMsg("Cannot add decimal to null");
                        }
                        break;
                    case "+-":
                        if (!RightNegative)
                        {
                            RightNegative = true;
                            RightString = "-" + RightString;
                            SetTotalStringCheckPower();
                        }
                        else
                        {
                            RightNegative = false;
                            RightString = RightString.Remove(0, 1);
                            SetTotalStringCheckPower();
                        }
                        break;

                    case "√":
                        if (!RightIsRoot)
                        {
                            RightIsRoot = true;
                            RightString = "√" + RightString;
                            SetTotalStringCheckPower();
                            canCalculate = true;
                        }
                        else
                        {
                            //RightIsRoot = false;
                            //RightString = RightString.Remove(0, 1);
                            //SetTotalStringCheckPower();
                            //canCalculate = false;
                            ErrorMsg("This is already a root");
                        }
                        break;

                    case "^":
                        if (!String.IsNullOrEmpty(RightString))
                        {
                            if (!RightHasPower)
                            {
                                RightHasPower = true;
                                TotalString += input;
                                canCalculate = true;
                            }
                            else
                            {
                                RightHasPower = false;
                                canCalculate = false;
                            }
                        }
                        else
                        {
                            ErrorMsg("Cannot calulate power of null");
                        }
                        break;

                    case "C":
                        Clear();
                        break;
                    case "=":
                        if (canCalculate)
                        {
                            MessageBox.Show(Calculate().ToString());
                        }
                        else
                        {
                            ErrorMsg("Cannot calculate with current input");
                        }
                        break;
                    default:
                        ErrorMsg("Cannot add this to current equation");
                        break;
                }
            }
            else if (RightHasPower)
            {
                switch (input)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        RightPowerString += input;
                        TotalString += input;
                        break;
                    case "0":
                        RightPowerString += Convert.ToInt32(input);
                        TotalString += input;
                        break;
                    case ",":
                        if (!String.IsNullOrEmpty(RightPowerString))
                        {
                            if (!RightPowerHasDecimal)
                            {
                                RightPowerString += input;
                                RightPowerHasDecimal = true;
                                SetTotalStringCheckPower();
                            }
                            else
                            {
                                ErrorMsg("This is already a decimal");
                            }
                        }
                        else
                        {
                            ErrorMsg("Cannot add decimal to null");
                        }
                        break;
                    case "+-":
                        if (!RightPowerNegative)
                        {
                            RightPowerNegative = true;
                            RightPowerString = "-" + RightPowerString;
                            SetTotalStringCheckPower();
                        }
                        else
                        {
                            RightPowerNegative = false;
                            RightPowerString = RightPowerString.Remove(0, 1);
                            SetTotalStringCheckPower();
                        }
                        break;
                    case "C":
                        Clear();
                        break;
                    case "=":
                        if (canCalculate)
                        {
                            MessageBox.Show(Calculate().ToString()); ;
                        }
                        else
                        {
                            ErrorMsg("Cannot calculate with current input");
                        }
                        break;
                    default:
                        ErrorMsg("Cannot add this to current equation");
                        break;
                }
            }
        }
        private void SetTotalStringCheckPower()
        {
            if (LeftHasPower && !RightHasPower)
            {
                TotalString = LeftString + "^" + LeftPowerString + op + RightString;

            }
            else if (!LeftHasPower && RightHasPower)
            {
                TotalString = LeftString + op + RightString + "^" + RightPowerString;
            }
            else if (LeftHasPower && RightHasPower)
            {
                TotalString = LeftString + "^" + LeftPowerString + op + RightString + "^" + RightPowerString;
            }
            else if (!LeftHasPower && !RightHasPower)
            {
                TotalString = LeftString + op + RightString;
            }
        }
        private void GetMemory()
        {

        }
        private double Calculate()
        {
            //Deze zijn voor calculaties zonder operator
            //negatieve root
            if (LeftIsRoot && LeftNegative && !LeftHasPower && !HasOperator)
            {
                NegativeRootLeft();
                Clear();
                return result;
            }
            // negatief, power in root
            else if (LeftIsRoot && LeftNegative && LeftHasPower && !HasOperator)
            {
                NegativePowerRootLeft();
                Clear();
                return result;
            }
            //power in root
            else if (LeftIsRoot && !LeftNegative && LeftHasPower && !HasOperator)
            {
                PowerRootLeft();
                Clear();
                return result;
            }
            //alleen root
            else if (LeftIsRoot && !LeftNegative && !LeftHasPower && !HasOperator)
            {
                RootLeft();
                Clear();
                return result;
            }
            //alleen power
            else if (LeftHasPower && !HasOperator && !LeftNegative) 
            {
                PowerLeft();
                Clear();
                return result;
            }
            //Power en negatief
            else if (LeftHasPower && LeftNegative && !HasOperator)
            {
                NegativePowerLeft();
                Clear();
                return result;
            }


            //Voor calculaties met wortels of machten aan de rechter kant 

            //aleen power rechts
            else if (HasOperator && !LeftIsRoot & !LeftHasPower && !RightNegative && RightHasPower && !RightIsRoot)
            {
                PowerRight();
                result = UseOperator(op, Convert.ToDouble(LeftString), Convert.ToDouble(result));
                Clear();
                return result;
            }
            //Negative power right
            else if (HasOperator && !LeftIsRoot & !LeftHasPower && RightNegative && RightHasPower && !RightIsRoot)
            {
                NegativePowerRight();
                result = UseOperator(op, Convert.ToDouble(LeftString), Convert.ToDouble(result));
                Clear();
                return result;
            }
            // Power in root rechts
            else if (HasOperator && !LeftIsRoot & !LeftHasPower && RightHasPower && RightIsRoot && !RightNegative)
            {
                PowerRootRight();
                result = UseOperator(op, Convert.ToDouble(LeftString), Convert.ToDouble(result));
                Clear();
                return result;
            }
            // Power in root rechts met negatieve
            else if (HasOperator && !LeftIsRoot & !LeftHasPower && RightHasPower && RightIsRoot && RightNegative)
            {
                NegativePowerRootRight();
                result = UseOperator(op, Convert.ToDouble(LeftString), Convert.ToDouble(result));
                Clear();
                return result;
            }
            //Negatieve root rechts
            else if (HasOperator && !LeftIsRoot & !LeftHasPower && !RightHasPower && RightIsRoot && RightNegative)
            {
                NegativeRootRight();
                result = UseOperator(op, Convert.ToDouble(LeftString), Convert.ToDouble(result));
                Clear();
                return result;
            }
            //Alleen root rechts
            else if (HasOperator && !LeftIsRoot & !LeftHasPower && !RightHasPower && RightIsRoot && !RightNegative)
            {
                RootRight();
                result = UseOperator(op, Convert.ToDouble(LeftString), Convert.ToDouble(result));
                Clear();
                return result;
            }

            //Voor calculaties met wortels of machten aan beide kanten

            //Left root right root
            else if (HasOperator && LeftIsRoot & !LeftHasPower &&! LeftNegative && !RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, RootLeft(), RootRight());
                Clear();
                return result;
            }
            //Left Root Right Power
            else if (HasOperator && LeftIsRoot & !LeftHasPower && !LeftNegative && !RightNegative && RightHasPower && !RightIsRoot)
            {
                result = UseOperator(op, RootLeft(), PowerRight());
                Clear();
                return result;
            }
            //Left root Right Negative Power
            else if (HasOperator && LeftIsRoot & !LeftHasPower && !LeftNegative && RightHasPower && !RightIsRoot && RightNegative)
            {
                result = UseOperator(op, RootLeft(), NegativePowerRight());
                Clear();
                return result;
            }
            // Left Root Right Negative/Nothing
            else if (HasOperator && LeftIsRoot && !LeftHasPower && !RightHasPower && !RightIsRoot)
            {
                result = UseOperator(op, RootLeft(), Convert.ToDouble(RightString));
                Clear();
                return result;
            }
            // Left Root Right PowerRoot
            else if (HasOperator && LeftIsRoot && !LeftHasPower && !LeftNegative && RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, RootLeft(), PowerRootRight());
                Clear();
                return result;
            }
            // Left Root Right Negative Root
            else if (HasOperator && LeftIsRoot && !LeftHasPower && !LeftNegative && !RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, RootLeft(), NegativeRootRight());
                Clear();
                return result;
            }
            // Left Root Right Negative Power Root
            else if (HasOperator && LeftIsRoot && !LeftHasPower && !LeftNegative && RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, RootLeft(), NegativePowerRootRight());
                Clear();
                return result;
            }

            // Left Power Right Root
            else if (HasOperator && !LeftIsRoot && LeftHasPower && !LeftNegative && !RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, PowerLeft(), RootRight());
                Clear();
                return result;
            }
            // Left Power RIght Negative/Nothing
            else if (HasOperator && !LeftIsRoot && !LeftNegative && LeftHasPower && !RightHasPower && !RightIsRoot)
            {
                result = UseOperator(op, PowerLeft(), Convert.ToDouble(RightString));
                Clear();
                return result;
            }
            // Left Power Right RootPower
            else if (HasOperator && !LeftIsRoot && !LeftNegative && LeftHasPower && RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, PowerLeft(), PowerRootRight());
                Clear();
                return result;
            }
            // Left Power Right Negative Root
            else if (HasOperator && !LeftIsRoot && !LeftNegative && LeftHasPower && !RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, PowerLeft(), NegativeRootRight());
                Clear();
                return result;
            }
            // Left Power Right Negative Root Power
            else if (HasOperator && !LeftIsRoot && LeftHasPower && !LeftNegative && RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, PowerLeft(), NegativePowerRootRight());
                Clear();
                return result;
            }
            //Left power Right power 
            else if (HasOperator && !LeftIsRoot & LeftHasPower && RightHasPower && !RightIsRoot && !LeftNegative && !RightNegative)
            {
                result = UseOperator(op, PowerLeft(), PowerRight());
                Clear();
                return result;
            }
            //Left Power Right Negative power
            else if (HasOperator && !LeftIsRoot & LeftHasPower && !LeftNegative && RightHasPower && !RightIsRoot && RightNegative)
            {
                result = UseOperator(op, PowerLeft(), NegativePowerRight());
                Clear();
                return result;
            }

            // Left Negative Power Right Root
            else if (HasOperator && !LeftIsRoot && LeftHasPower && LeftNegative && !RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, NegativePowerLeft(), RootRight());
                Clear();
                return result;
            }
            // Left Negative Power RIght Negative/Nothing
            else if (HasOperator && !LeftIsRoot && LeftNegative && LeftHasPower && !RightHasPower && !RightIsRoot)
            {
                result = UseOperator(op, NegativePowerLeft(), Convert.ToDouble(RightString));
                Clear();
                return result;
            }
            // Left Negative Power Right RootPower
            else if (HasOperator && !LeftIsRoot && LeftNegative && LeftHasPower && RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, NegativePowerLeft(), PowerRootRight());
                Clear();
                return result;
            }
            // Left Negative Power Right Negative Root
            else if (HasOperator && !LeftIsRoot && LeftNegative && LeftHasPower && !RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativePowerLeft(), NegativeRootRight());
                Clear();
                return result;
            }
            // Left Negative Power Right Negative Root Power
            else if (HasOperator && !LeftIsRoot && LeftHasPower && LeftNegative && RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativePowerLeft(), NegativePowerRootRight());
                Clear();
                return result;
            }
            //Left Negative power Right power 
            else if (HasOperator && !LeftIsRoot & LeftHasPower && RightHasPower && !RightIsRoot && LeftNegative && !RightNegative)
            {
                result = UseOperator(op, NegativePowerLeft(), PowerRight());
                Clear();
                return result;
            }
            //Left Negative Power Right Negative power
            else if (HasOperator && !LeftIsRoot & LeftHasPower && LeftNegative && RightHasPower && !RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativePowerLeft(), NegativePowerRight());
                Clear();
                return result;
            }


            // Left RootPower Right Root
            else if (HasOperator && LeftIsRoot && LeftHasPower && !LeftNegative && !RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, PowerRootLeft(), RootRight());
                Clear();
                return result;
            }
            // Left RootPower Right Power
            else if (HasOperator && LeftIsRoot && LeftHasPower && !LeftNegative && RightHasPower && !RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, PowerRootLeft(), PowerRight());
                Clear();
                return result;
            }
            // Left RootPower Right Negative Power
            else if (HasOperator && LeftIsRoot && LeftHasPower && !LeftNegative && RightHasPower && !RightIsRoot && RightNegative)
            {
                result = UseOperator(op, PowerRootLeft(), NegativePowerRight());
                Clear();
                return result;
            }
            // Left RootPower Right Negative/Nothing
            else if (HasOperator && LeftIsRoot && LeftHasPower && !LeftNegative && !RightHasPower && !RightIsRoot)
            {
                result = UseOperator(op, PowerRootLeft(), Convert.ToDouble(RightString));
                Clear();
                return result;
            }
            // Left RootPower Right Root Power
            else if (HasOperator && LeftIsRoot && LeftHasPower && !LeftNegative && RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, PowerRootLeft(), PowerRootRight());
                Clear();
                return result;
            }
            // Left RootPower Right Negative Root
            else if (HasOperator && LeftIsRoot && LeftHasPower && !LeftNegative && !RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, PowerRootLeft(), NegativeRootRight());
                Clear();
                return result;
            }
            // Left RootPower Right Negative Root Power
            else if (HasOperator && LeftIsRoot && LeftHasPower && !LeftNegative && RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, PowerRootLeft(), NegativePowerRootRight());
                Clear();
                return result;
            }

            // Left Negtative Root Right Root
            else if (HasOperator && LeftIsRoot && !LeftHasPower && LeftNegative && !RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, NegativeRootLeft(), RootRight());
                Clear();
                return result;
            }
            // Left Negtative Root Right Power
            else if (HasOperator && LeftIsRoot && !LeftHasPower && LeftNegative && RightHasPower && !RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, NegativeRootLeft(), PowerRight());
                Clear();
                return result;
            }
            // Left Negtative Root Right Negative Power
            else if (HasOperator && LeftIsRoot && !LeftHasPower && LeftNegative && RightHasPower && !RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativeRootLeft(), NegativePowerRight());
                Clear();
                return result;
            }
            // Left Negtative Root Right Negative/Nothing 
            else if (HasOperator && LeftIsRoot && !LeftHasPower && LeftNegative && !RightHasPower && !RightIsRoot)
            {
                result = UseOperator(op, NegativeRootLeft(), Convert.ToDouble(RightString));
                Clear();
                return result;
            }
            // Left Negtative Root Right RootPower
            else if (HasOperator && LeftIsRoot && !LeftHasPower && LeftNegative && RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, NegativeRootLeft(), PowerRootRight());
                Clear();
                return result;
            }
            // Left Negtative Root Right Negative Root
            else if (HasOperator && LeftIsRoot && !LeftHasPower && LeftNegative && !RightHasPower && !RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativeRootLeft(), NegativeRootRight());
                Clear();
                return result;
            }
            // Left Negtative Root Right Negative Power Root
            else if (HasOperator && LeftIsRoot && !LeftHasPower && LeftNegative && RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativeRootLeft(), NegativePowerRootRight());
                Clear();
                return result;
            }

            // Left Negtative Power Root Right Root
            else if (HasOperator && LeftIsRoot && LeftHasPower && LeftNegative && !RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, NegativePowerRootLeft(), RootRight());
                Clear();
                return result;
            }
            // Left Negtative Power Root Right Power
            else if (HasOperator && LeftIsRoot && LeftHasPower && LeftNegative && RightHasPower && !RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, NegativePowerRootLeft(), PowerRight());
                Clear();
                return result;
            }
            // Left Negtative Power Root Right Negative Power
            else if (HasOperator && LeftIsRoot && LeftHasPower && LeftNegative && RightHasPower && !RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativePowerRootLeft(), NegativePowerRight());
                Clear();
                return result;
            }
            // Left Negtative Power Root Right Negative/Nothing
            else if (HasOperator && LeftIsRoot && LeftHasPower && LeftNegative && !RightHasPower && !RightIsRoot)
            {
                result = UseOperator(op, NegativePowerRootLeft(), Convert.ToDouble(RightString));
                Clear();
                return result;
            }
            // Left Negtative Power Root Right Root Power
            else if (HasOperator && LeftIsRoot && LeftHasPower && LeftNegative && RightHasPower && RightIsRoot && !RightNegative)
            {
                result = UseOperator(op, NegativePowerRootLeft(), PowerRootRight());
                Clear();
                return result;
            }
            // Left Negtative Power Root Right Negative Root
            else if (HasOperator && LeftIsRoot && LeftHasPower && LeftNegative && !RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativePowerRootLeft(), NegativeRootRight());
                Clear();
                return result;
            }
            // Left Negtative Power Root Right Negative Power Root
            else if (HasOperator && LeftIsRoot && LeftHasPower && LeftNegative && RightHasPower && RightIsRoot && RightNegative)
            {
                result = UseOperator(op, NegativePowerRootLeft(), NegativePowerRootRight());
                Clear();
                return result;
            }

            //voor calculaties met operator zonder wortels of machten
            else
            {
                result = UseOperator(op, Convert.ToDouble(LeftString), Convert.ToDouble(RightString));
                Clear();
                return result;
            }
            return 0;
        }
        private double RootRight()
        {
            RightString = RightString.Remove(0, 1);
            result = Convert.ToDouble(RightString);
            result = Math.Sqrt(result);
            return result;
        }
        private double NegativeRootRight()
        {
            string firstChar = RightString.Substring(0, 1);
            if (firstChar == "√")
            {
                RightString = RightString.Remove(0, 1);
                result = Convert.ToDouble(RightString);
                result = Math.Sqrt(result);
                return result;
            }
            else if (firstChar == "-")
            {
                RightString = RightString.Remove(0, 2);
                result = Convert.ToDouble(RightString);
                result = Math.Sqrt(result);
                result = result * -1;
                return result;
            }
            else
            {
                return 0;
            }
        }
        private double NegativePowerRootRight()
        {
            string firstChar = RightString.Substring(0, 1);
            if (firstChar == "√")
            {
                RightString = RightString.Remove(0, 2);
                result = Math.Pow(Convert.ToDouble(RightString), Convert.ToDouble(RightPowerString));
                result = result * -1;
                result = Math.Sqrt(result);
                return result;
            }
            else if (firstChar == "-")
            {
                RightString = RightString.Remove(0, 2);
                result = Math.Pow(Convert.ToDouble(RightString), Convert.ToDouble(RightPowerString));
                result = Math.Sqrt(result);
                result = result * -1;
                return result;
            }
            else
            {
                return 0;
            }
        }
        private double PowerRootRight()
        {
            RightString = RightString.Remove(0, 1);
            result = Math.Pow(Convert.ToDouble(RightString), Convert.ToDouble(RightPowerString));
            result = Math.Sqrt(result);
            return result;
        }
        private double PowerRight()
        {
            result = Math.Pow(Convert.ToDouble(RightString), Convert.ToDouble(RightPowerString));
            //Meet negatieve getallen nog aanpassen
            return result;
        }
        private double NegativePowerRight()
        {
            RightString = RightString.Remove(0, 1);
            result = Math.Pow(Convert.ToDouble(RightString), Convert.ToDouble(RightPowerString));
            result = -1 * result;
            //Meet negatieve getallen nog aanpassen
            return result;
        }

        private double PowerLeft()
        {
            result = Math.Pow(Convert.ToDouble(LeftString), Convert.ToDouble(LeftPowerString));
            //Meet negatieve getallen nog aanpassen
            return result;
        }
        private double NegativePowerLeft()
        {
            LeftString = LeftString.Remove(0, 1);
            result = Math.Pow(Convert.ToDouble(LeftString), Convert.ToDouble(LeftPowerString));
            result = -1 * result;
            //Meet negatieve getallen nog aanpassen
            return result;
        }
        private double RootLeft()
        {
            LeftString = LeftString.Remove(0, 1);
            result = Convert.ToDouble(LeftString);
            result = Math.Sqrt(result);
            return result;
        }
        private double PowerRootLeft()
        {
            LeftString = LeftString.Remove(0, 1);
            result = Math.Pow(Convert.ToDouble(LeftString), Convert.ToDouble(LeftPowerString));
            result = Math.Sqrt(result);
            return result;
        }
        private double NegativeRootLeft()
        {
            string firstChar = LeftString.Substring(0, 1);
            if (firstChar == "√")
            {
                LeftString = LeftString.Remove(0, 1);
                result = Convert.ToDouble(LeftString);
                result = Math.Sqrt(result);
                return result;
            }
            else if (firstChar == "-")
            {
                LeftString = LeftString.Remove(0, 2);
                result = Convert.ToDouble(LeftString);
                result = Math.Sqrt(result);
                result = result * -1;
                return result;
            }
            else
            {
                return 0;
            }
        }
        private double NegativePowerRootLeft()
        {
            string firstChar = LeftString.Substring(0, 1);
            if (firstChar == "√")
            {
                LeftString = LeftString.Remove(0, 2);
                result = Math.Pow(Convert.ToDouble(LeftString), Convert.ToDouble(LeftPowerString));
                result = result * -1;
                result = Math.Sqrt(result);
                return result;
            }
            else if (firstChar == "-")
            {
                LeftString = LeftString.Remove(0, 2);
                result = Math.Pow(Convert.ToDouble(LeftString), Convert.ToDouble(LeftPowerString));
                result = Math.Sqrt(result);
                result = result * -1;
                return result;
            }
            else
            {
                return 0;
            }
        }
        private double UseOperator(string ope, double val1, double val2)
        {
            switch (op)
            {
                case "*":
                    return val1 * val2;
                    break;
                case "/":
                    return val1 / val2;
                    break;
                case "-":
                    return val1 - val2;
                    break;
                case "+":
                    return val1 + val2;
                    break;
                default:
                    return result;
                    break;
            }
        }
        private void Operator(string input)
        {
            HasOperator = true;
            op = input;
        }
        private void Clear()
        {
            LeftNegative = false;
            RightNegative = false;
            TotalString = string.Empty;
            LeftString = string.Empty;
            RightString = string.Empty;
            LeftIsRoot = false;
            canCalculate = false;
            op = string.Empty;
            HasOperator = false;
            HasDecimalRight = false;
            HasDecimalLeft = false;
            LeftPowerHasDecimal= false;
            LeftHasPower = false;
            LeftPowerString = string.Empty;
            LeftPowerNegative = false;
            TotalString = string.Empty;
            RightIsRoot = false;
            RightHasPower = false;
            RightPowerNegative = false;
            RightPowerString = string.Empty;
            RightPowerHasDecimal = false;
        }
        private void ErrorMsg(string ErrorType)
        {
            MessageBox.Show(ErrorType);
        }
    }
}
