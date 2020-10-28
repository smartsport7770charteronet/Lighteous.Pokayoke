using com.Lighteous.Pokayoke.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace com.Lighteous.Pokayoke {
    public class EntityPokayoke<T> : PokayokeBase {

        public EntityPokayoke(T entity) : base() { Entity = entity; Title = typeof(T).Name; }

        public T Entity { get; set; }

        public List<AttributePokayokeResult> AttributePokayokeResults { get; set; } = new List<AttributePokayokeResult>();
        public List<IEvent> Events { get; set; } = new List<IEvent>();

        // https://code-examples.net/en/q/8811c
        public override void Test() {

            try { 
                if(Entity != null) {
                    var props = Entity.GetType().GetProperties();
                    if (props != null && props.Any()) {
                        foreach (var prop in props) {
                            if(prop != null) {
                                if(prop.CustomAttributes != null && prop.CustomAttributes.Any()) {

                                    #region attribute logic
                                    var requiredAttr = prop.GetCustomAttribute(typeof(RequiredAttribute));
                                    if(requiredAttr != null) {
                                        var attrPokayokeResult = new AttributePokayokeResult();
                                        // Complete: validate and log event
                                        var requiredValue = prop.GetValue(Entity) == null ? string.Empty: prop.GetValue(Entity).ToString();
                                        var requiredAttribute = requiredAttr as RequiredAttribute;
                                        attrPokayokeResult.Attribute = requiredAttribute;
                                        if(string.IsNullOrEmpty(requiredValue)) {
                                            Events.Add(new Event($"{prop.Name} Required: Fail"));
                                            attrPokayokeResult.Pass = false;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                            if (!string.IsNullOrEmpty(requiredAttribute.ErrorMessage)) { 
                                                Events.Add(new ErrorEvent($"{prop.Name} Required Error: {requiredAttribute.ErrorMessage}"));
                                            }
                                        }
                                        else {
                                            Events.Add(new Event($"{prop.Name} Required: Pass"));
                                            attrPokayokeResult.Pass = true;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                        }
                                        AttributePokayokeResults.Add(attrPokayokeResult);
                                    }

                                    var regexAttr = prop.GetCustomAttribute(typeof(RegularExpressionAttribute));
                                    if(regexAttr != null) {
                                        // Complete: validate and log event
                                        var regexAttribute = regexAttr as RegularExpressionAttribute;
                                        var attrPokayokeResult = new AttributePokayokeResult();
                                        attrPokayokeResult.Attribute = regexAttribute;
                                        var regex = new Regex(regexAttribute.Pattern);
                                        var regexValue = prop.GetValue(Entity) == null ? string.Empty : prop.GetValue(Entity).ToString();
                                        var matchInfo = regex.Match(regexValue);
                                        if(matchInfo.Success) {
                                            Events.Add(new Event($"{prop.Name} Regular Expression: Pass"));
                                            attrPokayokeResult.Pass = true;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                        }
                                        else {
                                            Events.Add(new Event($"{prop.Name} Regular Expression: Fail"));
                                            attrPokayokeResult.Pass = false;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                            if (!string.IsNullOrEmpty(regexAttribute.ErrorMessage)) { 
                                                Events.Add(new ErrorEvent($"{prop.Name} Regular Expression Error: {regexAttribute.ErrorMessage}"));
                                            }
                                        }
                                        AttributePokayokeResults.Add(attrPokayokeResult);
                                    }

                                    var strLenAttr = prop.GetCustomAttribute(typeof(StringLengthAttribute));
                                    if(strLenAttr != null) {
                                        // Complete: validate and log event
                                        var strLenAttribute = strLenAttr as StringLengthAttribute;
                                        var attrPokayokeResult = new AttributePokayokeResult();
                                        attrPokayokeResult.Attribute = strLenAttribute;
                                        var strLenValue = prop.GetValue(Entity) == null ? string.Empty : prop.GetValue(Entity).ToString();
                                        var strLength = strLenValue.Length;
                                        if(strLength <= strLenAttribute.MaximumLength && strLength >= strLenAttribute.MinimumLength) {
                                            Events.Add(new Event($"{prop.Name} String Length: Pass"));
                                            attrPokayokeResult.Pass = true;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                        }
                                        else {
                                            Events.Add(new Event($"{prop.Name} String Length: Fail"));
                                            attrPokayokeResult.Pass = false;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                            if (!string.IsNullOrEmpty(strLenAttribute.ErrorMessage)) {
                                                Events.Add(new ErrorEvent($"{prop.Name} String Length Error: {strLenAttribute.ErrorMessage}"));
                                            }
                                        }
                                        AttributePokayokeResults.Add(attrPokayokeResult);
                                    }

                                    var maxLenAttr = prop.GetCustomAttribute(typeof(MaxLengthAttribute));
                                    if(maxLenAttr != null) {
                                        // Complete: validate and log event
                                        var maxLenAttribute = maxLenAttr as MaxLengthAttribute;
                                        var maxLenValue = prop.GetValue(Entity) == null ? string.Empty : prop.GetValue(Entity).ToString();
                                        var valueLength = maxLenValue.Length;
                                        if(valueLength <= maxLenValue.Length) {
                                            Events.Add(new Event($"{prop.Name} Max Length: Pass"));
                                        }
                                        else {
                                            Events.Add(new Event($"{prop.Name} Max Length: Fail"));
                                            if (!string.IsNullOrEmpty(maxLenAttribute.ErrorMessage)) {
                                                Events.Add(new ErrorEvent($"{prop.Name} Max Length Error: {maxLenAttribute.ErrorMessage}"));
                                            }
                                        }
                                    }

                                    var minLenAttr = prop.GetCustomAttribute(typeof(MinLengthAttribute));
                                    if(minLenAttr != null) {
                                        // Complete: validate and log event
                                        var minLenAttribute = minLenAttr as MinLengthAttribute;
                                        var attrPokayokeResult = new AttributePokayokeResult();
                                        attrPokayokeResult.Attribute = minLenAttribute;
                                        var minLenValue = prop.GetValue(Entity) == null ? string.Empty : prop.GetValue(Entity).ToString();
                                        var valueLength = minLenValue.Length;
                                        if (valueLength >= minLenAttribute.Length) {
                                            Events.Add(new Event($"{prop.Name} Min Length: Pass"));
                                            attrPokayokeResult.Pass = true;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                        }
                                        else {
                                            Events.Add(new Event($"{prop.Name} Min Length: Fail"));
                                            attrPokayokeResult.Pass = false;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                            if (!string.IsNullOrEmpty(minLenAttribute.ErrorMessage)) {
                                                Events.Add(new ErrorEvent($"{prop.Name} Min Length Error: {minLenAttribute.ErrorMessage}"));
                                            }
                                        }
                                        AttributePokayokeResults.Add(attrPokayokeResult);
                                    }

                                    var rangeAttr = prop.GetCustomAttribute(typeof(RangeAttribute));
                                    if(rangeAttr != null) {
                                        // Complete: validate and log event
                                        var rangeAttribute = rangeAttr as RangeAttribute;
                                        var attrPokayokeResult = new AttributePokayokeResult();
                                        attrPokayokeResult.Attribute = rangeAttribute;
                                        var rangeAttrValue = prop.GetValue(Entity) == null ? (int?)null : Convert.ToInt32(prop.GetValue(Entity));
                                        if(rangeAttrValue >= Convert.ToInt32(rangeAttribute.Minimum) && rangeAttrValue <= Convert.ToInt32(rangeAttribute.Maximum)) {
                                            Events.Add(new Event($"{prop.Name} Range: Pass"));
                                            attrPokayokeResult.Pass = true;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                        }
                                        else {
                                            Events.Add(new Event($"{prop.Name} Range: Fail"));
                                            attrPokayokeResult.Pass = false;
                                            attrPokayokeResult.Fail = !attrPokayokeResult.Pass;
                                            if (!string.IsNullOrEmpty(rangeAttribute.ErrorMessage)) {
                                                Events.Add(new ErrorEvent($"{prop.Name} Range Error: {rangeAttribute.ErrorMessage}"));
                                            }
                                        }
                                        AttributePokayokeResults.Add(attrPokayokeResult);
                                    }
                                    #endregion attribute logic

                                }
                            }
                        }
                    }

                    if(AttributePokayokeResults != null && AttributePokayokeResults.Any()) {
                        Pass = AttributePokayokeResults.All(are => are.Pass);
                        Fail = !Pass;
                    }
                }
            }
            catch(Exception err) {
                Pass = false;
                Fail = !Pass;
                Errors = ErrorMessage.Coalesce(err);
            }
        }

        public class AttributePokayokeResult {
            public AttributePokayokeResult() { }

            public Attribute Attribute { get; set; }

            public bool Pass { get; set; }
            public bool Fail { get; set; }
        }

    }
}
