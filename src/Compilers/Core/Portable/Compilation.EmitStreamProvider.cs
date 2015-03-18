﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.IO;

namespace Microsoft.CodeAnalysis
{
    /// <summary>
    /// The compilation object is an immutable representation of a single invocation of the
    /// compiler. Although immutable, a compilation is also on-demand, and will realize and cache
    /// data as necessary. A compilation can produce a new compilation from existing compilation
    /// with the application of small deltas. In many cases, it is more efficient than creating a
    /// new compilation from scratch, as the new compilation can reuse information from the old
    /// compilation.
    /// </summary>
    public abstract partial class Compilation
    {
        /// <summary>
        /// Abstraction that allows the caller to delay the creation of the <see cref="Stream"/> values 
        /// until they are actually needed.
        /// </summary>
        internal abstract class EmitStreamProvider
        {
            /// <summary>
            /// This method will be called once during Emit at the time the Compilation needs the
            /// associated <see cref="Stream"/> for writing.  It will not be called in the case of
            /// user errors in code.
            /// </summary>
            public abstract Stream GetStream(DiagnosticBag diagnostics);
        }

        internal sealed class SimpleEmitStreamProvider : EmitStreamProvider
        {
            private readonly Stream _stream;

            internal SimpleEmitStreamProvider(Stream stream)
            {
                _stream = stream;
            }

            public override Stream GetStream(DiagnosticBag diagnostics)
            {
                return _stream;
            }
        }
    }
}
