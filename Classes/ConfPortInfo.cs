//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class ConfPortInfo : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal ConfPortInfo(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(ConfPortInfo obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~ConfPortInfo() {
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
          pjsua2PINVOKE.delete_ConfPortInfo(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public int portId {
    set {
      pjsua2PINVOKE.ConfPortInfo_portId_set(swigCPtr, value);
    } 
    get {
      int ret = pjsua2PINVOKE.ConfPortInfo_portId_get(swigCPtr);
      return ret;
    } 
  }

  public string name {
    set {
      pjsua2PINVOKE.ConfPortInfo_name_set(swigCPtr, value);
      if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = pjsua2PINVOKE.ConfPortInfo_name_get(swigCPtr);
      if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public MediaFormatAudio format {
    set {
      pjsua2PINVOKE.ConfPortInfo_format_set(swigCPtr, MediaFormatAudio.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.ConfPortInfo_format_get(swigCPtr);
      MediaFormatAudio ret = (cPtr == global::System.IntPtr.Zero) ? null : new MediaFormatAudio(cPtr, false);
      return ret;
    } 
  }

  public float txLevelAdj {
    set {
      pjsua2PINVOKE.ConfPortInfo_txLevelAdj_set(swigCPtr, value);
    } 
    get {
      float ret = pjsua2PINVOKE.ConfPortInfo_txLevelAdj_get(swigCPtr);
      return ret;
    } 
  }

  public float rxLevelAdj {
    set {
      pjsua2PINVOKE.ConfPortInfo_rxLevelAdj_set(swigCPtr, value);
    } 
    get {
      float ret = pjsua2PINVOKE.ConfPortInfo_rxLevelAdj_get(swigCPtr);
      return ret;
    } 
  }

  public IntVector listeners {
    set {
      pjsua2PINVOKE.ConfPortInfo_listeners_set(swigCPtr, IntVector.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.ConfPortInfo_listeners_get(swigCPtr);
      IntVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new IntVector(cPtr, false);
      return ret;
    } 
  }

  public ConfPortInfo() : this(pjsua2PINVOKE.new_ConfPortInfo(), true) {
  }

}
