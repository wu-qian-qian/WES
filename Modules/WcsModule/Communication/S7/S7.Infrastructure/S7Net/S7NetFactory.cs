using Common.Application.NetWork.Other.Base;
using Common.Application.NetWork.Other.Config;
using S7.Application.Abstractions;
using S7.Application.Abstractions.Data;

namespace S7.Infrastructure.S7Net;

public class S7NetFactory : IS7NetFactory
{
    public INet CraetNet(S7NetModel netConfig, Action<string> logAciont = null)
    {
         return new S7NetToken(netConfig,logAciont);
    }
}