using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Clockwork.Ini.Core
{
    internal static class IniParser
    {
        internal static IniData Parse(StreamReader reader)
        {
            IniData data = new IniData();

            int line = 0;
            string lineData = "";

            int currentStore = data.Global.ID;

            while ((lineData = reader.ReadLine()) != null)
            {
                line++;
                ParserState state = ParserState.GettingToName;

                if(lineData.Length > 0)
                {
                    switch (lineData[0])
                    {
                        case '[':
                            string rawKey = lineData.Substring(1, lineData.Length - 2);

                            string[] keyData = rawKey.Split(".");

                            DataStore store = ResolveKeyData(data.Global, new List<string>(keyData));
                            currentStore = store.ID;
                            break;

                        case ';':
                            continue;

                        default:
                            string valueName = "";
                            string valueString = "";

                            bool inString = false;

                            for (int i = 0; i < lineData.Length; i++)
                            {
                                char c = lineData[i];

                                switch (state)
                                {
                                    case ParserState.GettingToName:
                                        if(c != ' ' && c != '\t')
                                        {
                                            state = ParserState.GettingName;
                                            valueName += c;
                                        }
                                        break;

                                    case ParserState.GettingName:
                                        if (c == ' ')
                                        {
                                            if (!Regex.IsMatch(valueName, @"^[a-zA-Z_][a-zA-Z0-9_]*$"))
                                                throw new IniParserException(line, i, "Invallid value name!");

                                            state = ParserState.GettingSeperator;
                                        }
                                        else
                                            valueName += c;
                                        break;

                                    case ParserState.GettingSeperator:
                                        if (c == '=')
                                            state = ParserState.GettingToValue;
                                        else if (c != ' ')
                                            throw new IniParserException(line, i, "Invallid value name!");
                                        break;

                                    case ParserState.GettingToValue:
                                        if (c != ' ')
                                        {
                                            state = ParserState.GettingValue;
                                            valueString += c;

                                            if (c == '"')
                                                inString = true;
                                        }
                                        break;

                                    case ParserState.GettingValue:
                                        if (c == ' ' && !inString)
                                            state = ParserState.GettingToEnd;
                                        else if (c == ';' && !inString)
                                            state = ParserState.EndingComment;
                                        else if (c == '"')
                                        {
                                            if (!inString)
                                                throw new IniParserException(line, i, "Invallid \"");

                                            valueString += c;
                                            inString = false;
                                        }
                                        else
                                            valueString += c;
                                        break;

                                    case ParserState.GettingToEnd:
                                        if (c == ';')
                                            state = ParserState.EndingComment;
                                        else if (c != ' ')
                                            throw new IniParserException(line, i, "Invalid value!");
                                        break;
                                }
                            }

                            if (inString)
                                throw new IniParserException(line, lineData.Length, "String value was not ended!");

                            try
                            {
                                data.DataStores[currentStore].SetValue(valueName, TypeManager.ParseString(valueString));
                            }
                            catch (InvalidCastException e)
                            {
                                throw new IniParserException(line, lineData.Length, "Value was not able to be parsed!\n  e: " + e.Message);
                            }

                            break;
                    }
                }
                
            }

            return data;
        }

        internal static DataStore ResolveKeyData(DataStore store, List<string> keys)
        {
            if(keys.Count > 0)
            {
                string key = keys[0];
                keys.RemoveAt(0);

                if(!store.ValueExists(key))
                    store.CreateDataStore(key);

                return ResolveKeyData((DataStore)store.GetValue(key), keys);
            }
            else
            {
                return store;
            }
        }
    }

    internal enum ParserState
    {
        GettingToName = 0,
        GettingName,
        GettingSeperator,
        GettingToValue,
        GettingValue,
        GettingToEnd,
        EndingComment
    }
}
