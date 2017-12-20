//using System;
//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

//namespace Tatbikat.UI.Extensions
//{
//    public class UIDirectionExtension : IMarkupExtension
//    {

//        private static GridLengthTypeConverter _gridLengthTypeConverter = new GridLengthTypeConverter();
//        private static LayoutOptionsConverter _layoutOptionsConverter = new LayoutOptionsConverter();
//        private static ThicknessTypeConverter _thicknessTypeConverter = new ThicknessTypeConverter();

//        public DataType Type { get; set; }
//        public object RTL { get; set; }
//        public object LTR { get; set; }


//        public object ProvideValue(IServiceProvider serviceProvider)
//        {

//            switch (Type)
//            {
//                case DataType.GridLength:
//                    {
//                        if (Surface.IsRTL)
//                        {
//                            return _gridLengthTypeConverter.ConvertFromInvariantString(RTL.ToString());
//                        }
//                        else
//                        {
//                            return _gridLengthTypeConverter.ConvertFromInvariantString(LTR.ToString());
//                        }
//                    }

//                case DataType.LayoutOption:
//                    {

//                        if (Surface.IsRTL)
//                        {
//                            return _layoutOptionsConverter.ConvertFromInvariantString(RTL.ToString());
//                        }
//                        else
//                        {
//                            return _layoutOptionsConverter.ConvertFromInvariantString(LTR.ToString());
//                        }

//                    }
//                case DataType.TextAlignment:
//                    {
//                        if (Surface.IsRTL)
//                        {
//                            return TryParseTextAlignement(RTL.ToString());
//                        }
//                        else
//                        {
//                            return TryParseTextAlignement(LTR.ToString());
//                        }

//                    }
//                case DataType.Int:
//                    {
//                        if (Surface.IsRTL)
//                        {
//                            return Convert.ToInt32(RTL);
//                        }
//                        else
//                        {
//                            return Convert.ToInt32(LTR);
//                        }
//                    }
//                case DataType.ResourceKey:

//                    if (Surface.IsRTL)
//                    {
//                        return LocalizeExtension.TranslateOrDefault(RTL.ToString());
//                    }
//                    else
//                    {
//                        return LocalizeExtension.TranslateOrDefault(LTR.ToString());
//                    }

//                case DataType.String:
//                    {
//                        if (Surface.IsRTL)
//                        {
//                            return RTL.ToString();
//                        }
//                        else
//                        {
//                            return LTR.ToString();
//                        }
//                    }
//                case DataType.Thickness:
//                    {
//                        if (Surface.IsRTL)
//                        {
//                            return _thicknessTypeConverter.ConvertFromInvariantString(RTL.ToString());
//                        }
//                        else
//                        {
//                            return _thicknessTypeConverter.ConvertFromInvariantString(LTR.ToString());
//                        }
//                    }
//                default:
//                    break;
//            }
//            return "";

//        }

//        private TextAlignment TryParseTextAlignement(string val)
//        {
//            bool parsingResult = Enum.TryParse<TextAlignment>(val, out TextAlignment alignemet);

//            if (!parsingResult)
//            {
//                throw new ZindRuntimeException($"{val} couldn't be recognized as TextAlignment.");
//            }

//            return alignemet;
//        }



//    }

//    public enum DataType
//    {
//        GridLength,
//        LayoutOption,
//        TextAlignment,
//        Int,
//        String,
//        ResourceKey,
//        Thickness
//    }


//}
