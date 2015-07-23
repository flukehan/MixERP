using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MixERP.Net.Framework.Contracts.Checklist
{
    public abstract class FirstStep
    {
        [Localizable(true)]
        public string Name { get; set; }
        public string Icon { get; set; }
        [Localizable(true)]
        public string Category { get; set; }
        public string CategoryAlias { get; set; }
        public int Order { get; set; }
        [Localizable(true)]
        public string Description { get; set; }
        public bool Status { get; set; }
        [Localizable(true)]
        public string Message { get; set; }
        public string NavigateUrl { get; set; }

        public static IEnumerable<FirstStep> GetAll()
        {
            Type type = typeof (FirstStep);
            
            var items = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsSubclassOf(type));

            return items.Select(t => (FirstStep) Activator.CreateInstance(t));
        }
    }
}