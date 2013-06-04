using System.Collections.Generic;
namespace Nasty.Core
{
    /**
     * This class keeps all specific data, sent with a particular event by
     * a javascript component.
     *
     * @author Stanislav Tkachev
     * @version 1.0
     *
     */
    public class EventArgs {

        /**
         * This is id of the javascript component, which triggered the event.
         */
        public string SrcId { get; set; }
	    private readonly IDictionary<string, string> _args = new Dictionary<string, string>();
	
	    public string this[string key] 
        {
            get { return _args[key]; }
            set { _args.Add(key, value); }
	    }
    }
}