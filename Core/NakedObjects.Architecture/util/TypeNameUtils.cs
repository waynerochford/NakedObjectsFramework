﻿// Copyright Naked Objects Group Ltd, 45 Station Road, Henley on Thames, UK, RG9 1AT
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.

using System;
using System.Linq;
using NakedObjects.Architecture.Spec;
using NakedObjects.Util;

namespace NakedObjects.Architecture.Util {
    public static class TypeNameUtils {
        public static string DecodeTypeName(string typeName, string separator = "-") {
            if (typeName.Contains("-")) {
                string rootType = typeName.Substring(0, typeName.IndexOf('`') + 2);
                string[] args = typeName.Substring(typeName.IndexOf('`') + 3).Split('-');

                Type genericType = TypeUtils.GetType(rootType);
                Type[] argTypes = args.Select(TypeUtils.GetType).ToArray();

                Type newType = genericType.MakeGenericType(argTypes);

                return newType.FullName;
            }

            return typeName;
        }

        public static string EncodeTypeName(this INakedObjectSpecification spec) {
            return EncodeTypeName(spec.FullName);
        }

        public static string EncodeTypeName(string typeName, string separator = "-") {
            Type type = TypeUtils.GetType(typeName);

            if (type.IsGenericType) {
                return EncodeGenericTypeName(type, separator);
            }

            return type.FullName;
        }

        public static string EncodeGenericTypeName(Type type, string separator = "-") {
            string rootType = type.GetGenericTypeDefinition().FullName;
            return type.GetGenericArguments().Aggregate(rootType, (s, t) => s + separator + t.FullName);
        }

        public static string GetShortName(string name) {
            name = name.Substring(name.LastIndexOf('.') + 1);
            if (name.LastIndexOf('`') > 0) {
                name = name.Substring(0, name.LastIndexOf('`'));
            }
            return name;
        }
    }
}