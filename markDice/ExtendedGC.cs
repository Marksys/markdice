using System;

namespace System
{
    /// <summary>
    /// Represents some Garbagge Collector extended functionalities.
    /// </summary>
    public static class ExtendedGC
    {
        /// <summary>
        /// Collects all the objects passed as argument.
        /// </summary>
        /// <param name="objs">Array of objects to be collected.</param>
        public static void Collect( params object[] objs )
        {
            foreach( object obj in objs )
            {
                if( obj != null )
                {
                    if( obj is IDisposable )
                    {
                        ( (IDisposable)obj ).Dispose();
                    }

                    GC.ReRegisterForFinalize( obj );
                }
            }

            GC.Collect();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            for( int i = 0; i < objs.Length; i++ )
            {
                objs[ i ] = null;
            }

            GC.Collect();
            GC.Collect();
        }
    }
}
