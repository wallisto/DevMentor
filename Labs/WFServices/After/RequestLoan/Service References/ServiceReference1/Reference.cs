﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RequestLoan.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IApprove")]
    public interface IApprove {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApprove/RequestApproval", ReplyAction="http://tempuri.org/IApprove/RequestApprovalResponse")]
        RequestLoan.ServiceReference1.RequestApprovalResponse RequestApproval(RequestLoan.ServiceReference1.RequestApprovalRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IApprove/RequestApproval", ReplyAction="http://tempuri.org/IApprove/RequestApprovalResponse")]
        System.Threading.Tasks.Task<RequestLoan.ServiceReference1.RequestApprovalResponse> RequestApprovalAsync(RequestLoan.ServiceReference1.RequestApprovalRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RequestApproval", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class RequestApprovalRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public double amount;
        
        public RequestApprovalRequest() {
        }
        
        public RequestApprovalRequest(double amount) {
            this.amount = amount;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="RequestApprovalResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class RequestApprovalResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool RequestApprovalResult;
        
        public RequestApprovalResponse() {
        }
        
        public RequestApprovalResponse(bool RequestApprovalResult) {
            this.RequestApprovalResult = RequestApprovalResult;
        }
    }
}
