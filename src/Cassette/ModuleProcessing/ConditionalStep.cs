﻿#region License
/*
Copyright 2011 Andrew Davey

This file is part of Cassette.

Cassette is free software: you can redistribute it and/or modify it under the 
terms of the GNU General Public License as published by the Free Software 
Foundation, either version 3 of the License, or (at your option) any later 
version.

Cassette is distributed in the hope that it will be useful, but WITHOUT ANY 
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with 
Cassette. If not, see http://www.gnu.org/licenses/.
*/
#endregion

using System;

namespace Cassette.ModuleProcessing
{
    public class ConditionalStep<T> : IModuleProcessor<T>
        where T : Module
    {
        public ConditionalStep(
            Func<Module, ICassetteApplication, bool> condition, 
            params IModuleProcessor<T>[] children)
        {
            this.condition = condition;
            this.children = children;
        }

        readonly Func<Module, ICassetteApplication, bool> condition;
        readonly IModuleProcessor<T>[] children;

        public void Process(T module, ICassetteApplication application)
        {
            if (condition(module, application) == false) return;

            ProcessEachChild(module, application);
        }

        void ProcessEachChild(T module, ICassetteApplication application)
        {
            foreach (var child in children)
            {
                child.Process(module, application);
            }
        }
    }
}

