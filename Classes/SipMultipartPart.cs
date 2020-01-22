//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 4.0.1
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class SipMultipartPart : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal SipMultipartPart(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(SipMultipartPart obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~SipMultipartPart() {
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
          pjsua2PINVOKE.delete_SipMultipartPart(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
    }
  }

  public SipHeaderVector headers {
    set {
      pjsua2PINVOKE.SipMultipartPart_headers_set(swigCPtr, SipHeaderVector.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.SipMultipartPart_headers_get(swigCPtr);
      SipHeaderVector ret = (cPtr == global::System.IntPtr.Zero) ? null : new SipHeaderVector(cPtr, false);
      return ret;
    } 
  }

  public SipMediaType contentType {
    set {
      pjsua2PINVOKE.SipMultipartPart_contentType_set(swigCPtr, SipMediaType.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = pjsua2PINVOKE.SipMultipartPart_contentType_get(swigCPtr);
      SipMediaType ret = (cPtr == global::System.IntPtr.Zero) ? null : new SipMediaType(cPtr, false);
      return ret;
    } 
  }

  public string body {
    set {
      pjsua2PINVOKE.SipMultipartPart_body_set(swigCPtr, value);
      if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
    } 
    get {
      string ret = pjsua2PINVOKE.SipMultipartPart_body_get(swigCPtr);
      if (pjsua2PINVOKE.SWIGPendingException.Pending) throw pjsua2PINVOKE.SWIGPendingException.Retrieve();
      return ret;
    } 
  }

  public SipMultipartPart() : this(pjsua2PINVOKE.new_SipMultipartPart(), true) {
  }

}
