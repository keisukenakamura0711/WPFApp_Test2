using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp_Test2.ViewModels
{
    internal class MainViewModel : NotificationObject
    {
        public MainViewModel()
        {
            this._calc = new Calculater();
        }

        /// <summry>
        /// 計算を行うオブジェクト
        /// </summry>
        private Calculater _calc;

        private string _lhs;
        /// <summry>
        /// 割られる数に指定される文字列を取得または設定します
        /// </summry>
        public string Lhs
        {
            get { return this._lhs; }
            set
            {
                if (SetProperty(ref this._lhs, value))
                {
                    this.DivCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _rhs;
        /// <summry>
        /// 割る数に指定される文字列を取得または設定します
        /// </summry>
        public string Rhs
        {
            get { return this._rhs; }
            set
            {
                if (SetProperty(ref this._rhs, value))
                {
                    this.DivCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string _result;
        /// <summry>
        /// 計算結果を文字列として取得します。
        /// </summry>
        public string Result
        {
            get { return this._result; }
            private set { SetProperty(ref this._result, value); }
        }

        private DelegateCommand _divCommand;
        /// <summry>
        /// 割り算コマンドを取得します。
        /// </summry>
        public DelegateCommand DivCommand
        {
            get
            {
                return this._divCommand ?? (this._divCommand = new DelegateCommand(_ => { OnDivision(); },
                                                                                   _ => { var dummy = 0.0;
                                                                                          if (!double.TryParse(this.Lhs, out dummy)) { return false; }
                                                                                          if (!double.TryParse(this.Rhs, out dummy)) { return false; }
                                                                                          return true;
                                                                                        }));
            }
        }

        /// <summry>
        /// 割り算を実行します。
        /// </summry>
        private void OnDivision()
        {
            this._calc.Lhs = double.Parse(this.Lhs);
            this._calc.Rhs = double.Parse(this.Rhs);
            this._calc.ExecuteDiv();
            this.Result = this._calc.Result.ToString();
        }
    }

    internal class Calculater
    {
        /// <summry>
        /// 被演算項を取得または設定します。
        /// </summry>
        public double Lhs { get; set; }

        /// <summry>
        /// 演算項を取得または設定します。
        /// </summry>
        public double Rhs { get; set; }

        /// <summry>
        /// 計算結果を取得します。
        /// </summry>
        public double Result { get; private set; }

        /// <summry>
        /// 割り算を行います。
        /// </summry>
        public void ExecuteDiv()
        {
            this.Result = this.Lhs / this.Rhs;
        }
    }
}
