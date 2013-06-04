using Nasty.Js;
using System;
namespace Nasty.Core
{
    /**
     * Interface for generating javascript expression in response to an exception
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public interface IExceptionHandler
    {

        IJsExpression Handle(Exception e);
    }
}