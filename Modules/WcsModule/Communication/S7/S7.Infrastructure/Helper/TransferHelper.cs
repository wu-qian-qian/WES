using System.Text;
using Common.Helper;
using Common.TransferBuffer;
using S7.Domain.Enums;

namespace S7.Infrastructure.Helper;
public static class TransferHelper
{
    /// <summary>
/// 数据类型的转换
/// </summary>
/// <param name="buffer"></param>
/// <param name="offset"></param>
/// <param name="bitOffset"></param>
/// <param name="s7DataType"></param>
/// <param name="array"></param>
/// <returns></returns>
/// <exception cref="ArgumentException"></exception>
    public static string TransferBufferToData(byte[] buffer, int offset, byte? bitOffset, S7DataTypeEnum s7DataType,byte? array)
    {
     var result = string.Empty;
     switch (s7DataType)
     {
         case S7DataTypeEnum.Bool:
         {
             var res = buffer.Skip(offset).Take(1).ToArray();
             result = TransferBufferHelper.ByteFromBool(res[0], bitOffset.Value).ToString();
             break;
         }
         case S7DataTypeEnum.Byte:
         {
             var res = buffer.Skip(offset).Take(1).ToArray();
             result = res[0].ToString();
             break;
         }
         case S7DataTypeEnum.Int:
         {
             var res = buffer.Skip(offset).Take(2).ToArray();
             result = TransferBufferHelper.IntFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.DInt:
         {
             var res = buffer.Skip(offset).Take(4).ToArray();
             result = TransferBufferHelper.DIntFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.Word:
         {
             var res = buffer.Skip(offset).Take(2).ToArray();
             result = TransferBufferHelper.WordFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.DWord:
         {
             var res = buffer.Skip(offset).Take(4).ToArray();
             result = TransferBufferHelper.DWordFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.Real:
         {
             var res = buffer.Skip(offset).Take(4).ToArray();
             result = TransferBufferHelper.RealFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.LReal:
         {
             var res = buffer.Skip(offset).Take(8).ToArray();
             result = TransferBufferHelper.LRealFromByteArray(res).ToString();
             break;
         }
         case S7DataTypeEnum.String:
         {
             var res = buffer.Skip(offset).Take(array.Value).ToArray();
             result = TransferBufferHelper.S7StringFromByteArray(res, Encoding.ASCII);
             break;
         }
         case S7DataTypeEnum.S7String:
         {
             var res = buffer.Skip(offset).Take(array.Value).ToArray();
             result = TransferBufferHelper.S7StringFromByteArray(res, Encoding.ASCII);
             break;
         }
         case S7DataTypeEnum.Array:
         {
             var res = buffer.Skip(offset).Take(array.Value).ToArray();
             var hexValues = Array.ConvertAll(res, b => b.ToString());
             result = string.Join(',', hexValues);
             break;
         }
         default:
            throw new ArgumentException("没有该类型注入");
     }

     return result;
 }

  public static byte[] TransferDataToBuffer(string data,S7DataTypeEnum s7DataType,int arrayLen=0)
    {
        
       switch (s7DataType)
                        {
                            case S7DataTypeEnum.Byte:
                            {
                                byte.TryParse(data, out var @byte);
                                return new[] { @byte };
                            }
                            case S7DataTypeEnum.Int:
                            {
                                short.TryParse(data, out var @short);
                                return TransferBufferHelper.IntToByteArray(@short);
    
                            }
                            case S7DataTypeEnum.DInt:
                            {
                                int.TryParse(data, out var result);
                                return TransferBufferHelper.DIntToByteArray(result);
                            }
                            case S7DataTypeEnum.Word:
                            {
                                ushort.TryParse(data, out var result);
                                return TransferBufferHelper.WordToByteArray(result);

                            }
                            case S7DataTypeEnum.DWord:
                            {
                                uint.TryParse(data, out var result);
                               return TransferBufferHelper.DWordToByteArray(result);
                            }
                            case S7DataTypeEnum.Real:
                            {
                                float.TryParse(data, out var result);
                               return TransferBufferHelper.RealToByteArray(result);
                            }
                            case S7DataTypeEnum.LReal:
                            {
                                double.TryParse(data, out var result);
                                return TransferBufferHelper.LRealToByteArray(result);
                            }
                            case S7DataTypeEnum.String:
                            {
                                return TransferBufferHelper.StringToByteArray(data,arrayLen);
                            }
                            case S7DataTypeEnum.S7String:
                            {
                               return
                                    TransferBufferHelper.S7StringToByteArray(data, arrayLen,
                                        Encoding.ASCII);
                            }
                             case S7DataTypeEnum.Array:
                            {
                              
                                var tempBuffer=IEnumerableHelper.StringToArray<byte>(data).ToArray();
                    if (tempBuffer.Length>arrayLen)
                    {
                      throw new ArgumentException($"数据长度超过配置长度无法进行转换");  
                    }
                                  var buffer=new byte[arrayLen];
                                Array.Copy( tempBuffer,buffer,tempBuffer.Length);
                                return buffer;
                            }
                      default :  throw new ArgumentException($"无法解析写入数据类型");
                        }
    }
}