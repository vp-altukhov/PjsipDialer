//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class VidCodecParam : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal VidCodecParam(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(VidCodecParam obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~VidCodecParam() {
    Dispose(false);
  }

  public void Dispose() {
    Dispose(true);
    global::System.GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          pjsua2PINVOKE.delete_VidCodecParam(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public pjmedia_dir dir {
    set {
      pjsua2PINVOKE.VidCodecParam_dir_set(swigCPtr, (int)value);
    } 
    get {
      pjmedia_dir ret = (pjmedia_dir)pjsua2PINVOKE.VidCodecParam_dir_get(swigCPtr);
      return ret;
    } 
  }

  public pjmedia_vid_packing packing {
    set {
      pjsua2PINVOKE.VidCodecParam_packing_set(swigCPtr, (int)value);
    } 
    get {
      pjmedia_vid_packing ret = (pjmedia_vid_packing)pjsua2PINVOKE.VidCodecParam_packing_get(swigCPtr);
      return ret;
    } 
  }

  public MediaFormatVideo encFmt {
    set {
      pjsua2PINVOKE.VidCodecParam_encFmt_set(swigCPtr, MediaFormatVideo.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.VidCodecParam_encFmt_get(swigCPtr);
      MediaFormatVideo ret = (cPtr == global::System.IntPtr.Zero) ? null : new MediaFormatVideo(cPtr, false);
      return ret;
    } 
  }

  public CodecFmtpVector encFmtp {
    set {
      pjsua2PINVOKE.VidCodecParam_encFmtp_set(swigCPtr, CodecFmtpVector.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.VidCodecParam_encFmtp_get(swigCPtr);
      CodecFmtpVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new CodecFmtpVector(cPtr, false);
      return ret;
    } 
  }

  public uint encMtu {
    set {
      pjsua2PINVOKE.VidCodecParam_encMtu_set(swigCPtr, value);
    } 
    get {
      uint ret = pjsua2PINVOKE.VidCodecParam_encMtu_get(swigCPtr);
      return ret;
    } 
  }

  public MediaFormatVideo decFmt {
    set {
      pjsua2PINVOKE.VidCodecParam_decFmt_set(swigCPtr, MediaFormatVideo.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.VidCodecParam_decFmt_get(swigCPtr);
      MediaFormatVideo ret = (cPtr == global::System.IntPtr.Zero) ? null : new MediaFormatVideo(cPtr, false);
      return ret;
    } 
  }

  public CodecFmtpVector decFmtp {
    set {
      pjsua2PINVOKE.VidCodecParam_decFmtp_set(swigCPtr, CodecFmtpVector.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.VidCodecParam_decFmtp_get(swigCPtr);
      CodecFmtpVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new CodecFmtpVector(cPtr, false);
      return ret;
    } 
  }

  public bool ignoreFmtp {
    set {
      pjsua2PINVOKE.VidCodecParam_ignoreFmtp_set(swigCPtr, value);
    } 
    get {
      bool ret = pjsua2PINVOKE.VidCodecParam_ignoreFmtp_get(swigCPtr);
      return ret;
    } 
  }

  public VidCodecParam() : this(pjsua2PINVOKE.new_VidCodecParam(), true) {
  }

}
