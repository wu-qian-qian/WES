using Common.Application.NetWork.Other.Base;
using Common.Application.NetWork.Other.Config;
using S7.Application.Abstractions.Data;

namespace S7.Application.Abstractions;
public interface IS7NetFactory
{
    INet CraetNet(S7NetModel netConfig, Action<string> logAciont = null);
}