using System.Runtime.InteropServices;

namespace MetaQuotes.Services
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct IPRange
    {

        [MarshalAs(UnmanagedType.U4)]
        public uint ip_from;// начало диапазона IP адресов

        [MarshalAs(UnmanagedType.U4)]
        public uint ip_to;// конец диапазона IP адресов

        [MarshalAs(UnmanagedType.U4)]
        public uint location_index;// индекс записи о местоположении

    };
}
