//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class AudDevErrorEvent : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal AudDevErrorEvent(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(AudDevErrorEvent obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~AudDevErrorEvent() {
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
          pjsua2PINVOKE.delete_AudDevErrorEvent(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public pjmedia_dir dir {
    set {
      pjsua2PINVOKE.AudDevErrorEvent_dir_set(swigCPtr, (int)value);
    } 
    get {
      pjmedia_dir ret = (pjmedia_dir)pjsua2PINVOKE.AudDevErrorEvent_dir_get(swigCPtr);
      return ret;
    } 
  }

  public int id {
    set {
      pjsua2PINVOKE.AudDevErrorEvent_id_set(swigCPtr, value);
    } 
    get {
      int ret = pjsua2PINVOKE.AudDevErrorEvent_id_get(swigCPtr);
      return ret;
    } 
  }

  public int status {
    set {
      pjsua2PINVOKE.AudDevErrorEvent_status_set(swigCPtr, value);
    } 
    get {
      int ret = pjsua2PINVOKE.AudDevErrorEvent_status_get(swigCPtr);
      return ret;
    } 
  }

  public AudDevErrorEvent() : this(pjsua2PINVOKE.new_AudDevErrorEvent(), true) {
  }

}
