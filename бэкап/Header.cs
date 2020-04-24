using System.Runtime.InteropServices;

namespace MetaQuotes.Services
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Header
    {
        [MarshalAs(UnmanagedType.I4)]
        public int version;// версия база данных

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string name;// название/префикс для базы данных

        [MarshalAs(UnmanagedType.U8)]
        public ulong timestamp;// время создания базы данных

        [MarshalAs(UnmanagedType.I4)]
        public int records;// общее количество записей

        [MarshalAs(UnmanagedType.U4)]
        public uint offset_ranges;// смещение относительно начала файла до начала списка записей с геоинформацией

        [MarshalAs(UnmanagedType.U4)]
        public uint offset_cities;// смещение относительно начала файла до начала индекса с сортировкой по названию городов

        [MarshalAs(UnmanagedType.U4)]
        public uint offset_locations;// смещение относительно начала файла до начала списка записей о местоположении
    };
}
