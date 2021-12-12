using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.Shared
{
    public static class StringEnum
    {

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="thisEnum">The value.</param>
        /// <returns></returns>
        public static string ToValue(this Enum thisEnum)
        {
            string output = null;
            var type = thisEnum.GetType();
            //
            // Check first in our cached results...
            // Look for our 'StringValueAttribute' in the field's custom attributes
            var fi = type.GetField(thisEnum.ToString());
            var attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            if (attrs != null && attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }

    public class StringValue : Attribute
    {
        public StringValue(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; }
    }

    public enum PhaseBasic
    {
        [StringValue("Destroy")] Destroy = 1,
        [StringValue("Completed")] Completed,
        [StringValue("Order")] Order,
        [StringValue("Quote")] Quote,
        [StringValue("Opportunity")] Opportunity
    }

    public enum PriorityBasic
    {
        Medium = 1,
        Emergency = 2
    }

    public enum HistoryActionType
    {
        Added,
        Updated,
        Deleted,
        Assign,
        Move
    }

    public enum AcionCard
    {
        Add,
        Update,
        Delete
    }
}
