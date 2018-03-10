﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    /// <summary>
    /// The interface used to represent something cage-able.
    /// </summary>
    public interface ICageable
    {
        /// <summary>
        /// The cage-able entity's display size.
        /// </summary>
        double DisplaySize { get; }

        /// <summary>
        /// The cage-able entity's resource key.
        /// </summary>
        string ResourceKey { get; }

        /// <summary>
        /// the cage-able entity
        /// </summary>
        int XPosition { get; }

        /// <summary>
        /// The he cage-able entity's Y position.
        /// </summary>
        int YPosition { get; }

        /// <summary>
        /// The direction the cage-able entity will move horizontally.
        /// </summary>
        HorizontalDirection XDirection { get; }

        /// <summary>
        /// The direction the the cage-able entity will move vertically.
        /// </summary>
        VerticalDirection YDirection { get; }

    }
}
