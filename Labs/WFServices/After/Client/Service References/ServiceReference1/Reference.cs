﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ApplyResponse", Namespace="http://schemas.datacontract.org/2004/07/RequestLoan")]
    [System.SerializableAttribute()]
    public partial class ApplyResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ApplicationIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double MonthlyRepaymentField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ApplicationId {
            get {
                return this.ApplicationIdField;
            }
            set {
                if ((this.ApplicationIdField.Equals(value) != true)) {
                    this.ApplicationIdField = value;
                    this.RaisePropertyChanged("ApplicationId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double MonthlyRepayment {
            get {
                return this.MonthlyRepaymentField;
            }
            set {
                if ((this.MonthlyRepaymentField.Equals(value) != true)) {
                    this.MonthlyRepaymentField = value;
                    this.RaisePropertyChanged("MonthlyRepayment");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://develop.com/", ConfigurationName="ServiceReference1.ILoanService")]
    public interface ILoanService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://develop.com/ILoanService/Apply", ReplyAction="http://develop.com/ILoanService/ApplyResponse")]
        Client.ServiceReference1.ApplyResponse Apply(double amount, int term);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://develop.com/ILoanService/Apply", ReplyAction="http://develop.com/ILoanService/ApplyResponse")]
        System.Threading.Tasks.Task<Client.ServiceReference1.ApplyResponse> ApplyAsync(double amount, int term);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://develop.com/ILoanService/Confirm", ReplyAction="http://develop.com/ILoanService/ConfirmResponse")]
        bool Confirm(int applicationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://develop.com/ILoanService/Confirm", ReplyAction="http://develop.com/ILoanService/ConfirmResponse")]
        System.Threading.Tasks.Task<bool> ConfirmAsync(int applicationId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoanServiceChannel : Client.ServiceReference1.ILoanService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoanServiceClient : System.ServiceModel.ClientBase<Client.ServiceReference1.ILoanService>, Client.ServiceReference1.ILoanService {
        
        public LoanServiceClient() {
        }
        
        public LoanServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LoanServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoanServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoanServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Client.ServiceReference1.ApplyResponse Apply(double amount, int term) {
            return base.Channel.Apply(amount, term);
        }
        
        public System.Threading.Tasks.Task<Client.ServiceReference1.ApplyResponse> ApplyAsync(double amount, int term) {
            return base.Channel.ApplyAsync(amount, term);
        }
        
        public bool Confirm(int applicationId) {
            return base.Channel.Confirm(applicationId);
        }
        
        public System.Threading.Tasks.Task<bool> ConfirmAsync(int applicationId) {
            return base.Channel.ConfirmAsync(applicationId);
        }
    }
}