﻿#region Copyright Simple Injector Contributors
/* The Simple Injector is an easy-to-use Inversion of Control library for .NET
 * 
 * Copyright (c) 2014 Simple Injector Contributors
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
 * associated documentation files (the "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO 
 * EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE 
 * USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion

namespace SimpleInjector.Integration.Wcf
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.ServiceModel;
    using System.Threading;

    internal static class InstanceContextExtensions
    {
        internal static WcfOperationScope BeginScope(this InstanceContext instanceContext)
        {
            var extension = instanceContext.Extensions.Find<SimpleInjectorInstanceContextExtension>();

            if (extension == null)
            {
                extension = new SimpleInjectorInstanceContextExtension(new WcfOperationScope());
                instanceContext.Extensions.Add(extension);
            }

            return extension.Scope;
        }

        internal static WcfOperationScope GetCurrentScope(this InstanceContext instanceContext)
        {
            if (instanceContext == null)
            {
                return null;
            }

            var extension = instanceContext.Extensions.Find<SimpleInjectorInstanceContextExtension>();

            return extension != null ? extension.Scope : null;
        }

        internal static WcfOperationScope RemoveScope(this InstanceContext instanceContext)
        {
            var extension = instanceContext.Extensions.Find<SimpleInjectorInstanceContextExtension>();

            if (extension != null)
            {
                var scope = extension.Scope;

                instanceContext.Extensions.Remove(extension);

                return scope;
            }

            return null;
        }

        private sealed class SimpleInjectorInstanceContextExtension : IExtension<InstanceContext>
        {
            internal readonly WcfOperationScope Scope;

            public SimpleInjectorInstanceContextExtension(WcfOperationScope scope)
            {
                this.Scope = scope;
            }

            public void Attach(InstanceContext owner)
            {
            }

            public void Detach(InstanceContext owner)
            {
            }
        }
    }
}