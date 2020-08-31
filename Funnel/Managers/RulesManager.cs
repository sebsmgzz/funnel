using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Funnel.Models;

namespace Funnel.Managers
{
    /// <summary>
    /// Manages I/O to the rules asset
    /// </summary>
    public sealed class RulesManager
    {

        #region Fields

        private static XDocument xml;

        private XName xRuleName = "Rule";
        private XName xNameName = "Name";
        private XName xLocationName = "LocationFolderName";
        private XName xDestinationName = "DestinationFolderName";
        private XName xAddressesName = "Addresses";
        private XName xAddressName = "Address";

        #endregion

        #region Properties

        /// <summary>
        /// Represents if the file is loaded or not
        /// </summary>
        public bool FileLoaded
        {
            get { return xml != null; }
        }

        /// <summary>
        /// The number of rules currently loaded
        /// </summary>
        public int Count
        {
            get { return xml.Root.Elements(xRuleName).Count<XElement>(); }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a rule
        /// </summary>
        /// <param name="rule">The rule to add</param>
        /// <returns>True when added, false otherwise</returns>
        public bool Add(Rule rule)
        {
            if(FileLoaded && rule != null)
            {
                XElement xRule = new XElement(xRuleName);
                xRule.SetAttributeValue(xNameName, rule.Name);
                xRule.SetAttributeValue(xLocationName, rule.LocationFolderName);
                xRule.SetAttributeValue(xDestinationName, rule.DestinationFolderName);
                XElement xAddresses = new XElement(xAddressesName);
                foreach (string address in rule.Addresses)
                {
                    XElement xAddress = new XElement(xAddressName)
                    {
                        Value = address
                    };
                    xAddresses.Add(xAddress);
                }
                xRule.Add(xAddresses);
                xml.Root.Add(xRule);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a rule
        /// </summary>
        /// <param name="rule">The rule to remove</param>
        /// <returns>True when removed, false otherwise</returns>
        public bool Remove(string ruleName)
        {
            if(FileLoaded)
            {
                IEnumerable<XElement> xRules = xml.Root.Elements();
                foreach (XElement xRule in xRules)
                {
                    string ruleNameFound = xRule.Attribute(xNameName).Value;
                    if (ruleNameFound == ruleName)
                    {
                        xRule.Remove();
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Finds the rule by name and updates its values
        /// </summary>
        /// <param name="rule">The rule to update</param>
        /// <returns>True when updated, false if the file is not loaded
        /// or if the rule is null </returns>
        public bool Update(Rule rule)
        {
            if (FileLoaded && rule != null)
            {
                IEnumerable<XElement> xRules = xml.Root.Elements(xRuleName);
                foreach(XElement xRule in xRules)
                {
                    string ruleNameFound = xRule.Attribute(xNameName).Value;
                    if(ruleNameFound == rule.Name)
                    {
                        xRule.Attribute(xLocationName).Value = rule.LocationFolderName;
                        xRule.Attribute(xDestinationName).Value = rule.DestinationFolderName;
                        XElement xAddresses = xRule.Element(xAddressesName);
                        xAddresses.RemoveNodes();
                        foreach (string address in rule.Addresses)
                        {
                            XElement xAddress = new XElement(xAddressName)
                            {
                                Value = address
                            };
                            xAddresses.Add(xAddress);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Finds a rule by its name
        /// </summary>
        /// <param name="ruleName">The name of the rule</param>
        /// <returns>The rule data</returns>
        public Rule Find(string ruleName)
        {
            if(FileLoaded)
            {
                IEnumerable<XElement> xRules = xml.Root.Elements();
                foreach (XElement xRule in xRules)
                {
                    if (xRule.Attribute(xNameName).Value == ruleName)
                    {
                        Rule rule = new Rule();
                        rule.Name = xRule.Attribute(xNameName).Value;
                        rule.LocationFolderName = xRule.Attribute(xLocationName).Value;
                        rule.DestinationFolderName = xRule.Attribute(xDestinationName).Value;
                        IEnumerable<XElement> xAddresses = xRule.Element(xAddressesName).Elements(xAddressName);
                        foreach (XElement xAddress in xAddresses)
                        {
                            rule.AddAddress(xAddress.Value);
                        }
                        return rule;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the list of rules
        /// </summary>
        /// <returns>A list containing the rules</returns>
        public List<Rule> Values()
        {
            List<Rule> rules = new List<Rule>();
            if (FileLoaded)
            {
                IEnumerable<XElement> xRules = xml.Root.Elements();
                foreach (XElement xRule in xRules)
                {
                    Rule rule = new Rule
                    {
                        Name = xRule.Attribute(xNameName).Value,
                        LocationFolderName = xRule.Attribute(xLocationName).Value,
                        DestinationFolderName = xRule.Attribute(xDestinationName).Value
                    };
                    IEnumerable<XElement> xAddresses = xRule.Element(xAddressesName).Elements(xAddressName);
                    foreach (XElement xAddress in xAddresses)
                    {
                        rule.AddAddress(xAddress.Value);
                    }
                    rules.Add(rule);
                }
            }
            return rules;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Loads the rules into the application
        /// </summary>
        /// <returns>True when loaded</returns>
        public static bool TryLoad(out string message)
        {
            FilesManager manager = new FilesManager();
            try
            {
                xml = XDocument.Load(manager.RulesFullPath);
            }
            catch(FileNotFoundException e)
            {
                using (Stream stream = manager.GetEmbeddedResourceStream(manager.RulesName))
                {
                    xml = XDocument.Load(stream);
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
            message = null;
            return true;
        }

        /// <summary>
        /// Saves the rules into the file
        /// </summary>
        /// <returns>True whens saved, false otherwise</returns>
        public static bool TrySave(out string message)
        {
            FilesManager manager = new FilesManager();
            try
            {
                xml.Save(manager.RulesFullPath);
            }
            catch (FileNotFoundException e)
            {
                File.Create(manager.RulesFullPath);
                xml.Save(manager.RulesFullPath);
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
            message = null;
            return true;
        }

        #endregion

    }
}
