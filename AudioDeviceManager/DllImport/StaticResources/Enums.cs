namespace AudioControl.Enum
{
    public enum EDataFlow
    {
        eRender,
        eCapture,
        eAll
    }

    internal enum ERole
    {
        eConsole,
        eMultimedia,
        eCommunications,
        ERole_enum_count
    }

    internal enum EStgmAccess
    {
        STGM_READ,
        STGM_WRITE,
        STGM_READWRITE
    }

    internal enum EPid : int
    {
        PID_FRIENDLY_NAME = 14
    }

    [Flags]
    public enum DeviceState : uint
    {
        Active = 0x00000001,
        Disabled = 0x00000002,
        NotPresent = 0x00000004,
        Unplugged = 0x00000008,
        MaskAll = 0x0000000F
    }

    internal enum tagCLSCTX
    {
        CLSCTX_INPROC_SERVER = 0x1,
        CLSCTX_INPROC_HANDLER = 0x2,
        CLSCTX_LOCAL_SERVER = 0x4,
        CLSCTX_INPROC_SERVER16 = 0x8,
        CLSCTX_REMOTE_SERVER = 0x10,
        CLSCTX_INPROC_HANDLER16 = 0x20,
        CLSCTX_RESERVED1 = 0x40,
        CLSCTX_RESERVED2 = 0x80,
        CLSCTX_RESERVED3 = 0x100,
        CLSCTX_RESERVED4 = 0x200,
        CLSCTX_NO_CODE_DOWNLOAD = 0x400,
        CLSCTX_RESERVED5 = 0x800,
        CLSCTX_NO_CUSTOM_MARSHAL = 0x1000,
        CLSCTX_ENABLE_CODE_DOWNLOAD = 0x2000,
        CLSCTX_NO_FAILURE_LOG = 0x4000,
        CLSCTX_DISABLE_AAA = 0x8000,
        CLSCTX_ENABLE_AAA = 0x10000,
        CLSCTX_FROM_DEFAULT_CONTEXT = 0x20000,
        CLSCTX_ACTIVATE_X86_SERVER = 0x40000,
        CLSCTX_ACTIVATE_32_BIT_SERVER,
        CLSCTX_ACTIVATE_64_BIT_SERVER = 0x80000,
        CLSCTX_ENABLE_CLOAKING = 0x100000,
        CLSCTX_APPCONTAINER = 0x400000,
        CLSCTX_ACTIVATE_AAA_AS_IU = 0x800000,
        CLSCTX_RESERVED6 = 0x1000000,
        CLSCTX_ACTIVATE_ARM32_SERVER = 0x2000000
    }
}
