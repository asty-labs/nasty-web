namespace Nasty.Js
{
    /**
    * Interface for all code wrappers, that need to be encoded to javascript
    *
    * @author Stanislav Tkachev
    * @version 1.0
    *
    */
    public interface IJsExpression
    {

        /**
         * Encodes the expression to javascript
         *
         * @return  javascript
         */
        string Encode();
    }
}