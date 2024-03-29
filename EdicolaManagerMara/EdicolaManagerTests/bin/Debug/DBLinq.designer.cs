﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EdicolaManager
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="EdicolaDatabase")]
	public partial class DBLinqDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertTipologia(Tipologia instance);
    partial void UpdateTipologia(Tipologia instance);
    partial void DeleteTipologia(Tipologia instance);
    partial void InsertPeriodico(Periodico instance);
    partial void UpdatePeriodico(Periodico instance);
    partial void DeletePeriodico(Periodico instance);
    partial void InsertCronologia(Cronologia instance);
    partial void UpdateCronologia(Cronologia instance);
    partial void DeleteCronologia(Cronologia instance);
    partial void InsertMagazine(Magazine instance);
    partial void UpdateMagazine(Magazine instance);
    partial void DeleteMagazine(Magazine instance);
    #endregion
		
		public DBLinqDataContext() : 
				base(global::EdicolaManager.Properties.Settings.Default.EdicolaDatabaseConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public DBLinqDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DBLinqDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DBLinqDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DBLinqDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Tipologia> Tipologias
		{
			get
			{
				return this.GetTable<Tipologia>();
			}
		}
		
		public System.Data.Linq.Table<Periodico> Periodicos
		{
			get
			{
				return this.GetTable<Periodico>();
			}
		}
		
		public System.Data.Linq.Table<Cronologia> Cronologias
		{
			get
			{
				return this.GetTable<Cronologia>();
			}
		}
		
		public System.Data.Linq.Table<Magazine> Magazines
		{
			get
			{
				return this.GetTable<Magazine>();
			}
		}
		
		public System.Data.Linq.Table<Dashboard> Dashboards
		{
			get
			{
				return this.GetTable<Dashboard>();
			}
		}
		
		public System.Data.Linq.Table<ViewHistory> ViewHistories
		{
			get
			{
				return this.GetTable<ViewHistory>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Tipologia")]
	public partial class Tipologia : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdTipologia;
		
		private string _Nome;
		
		private System.Nullable<int> _Giorni;
		
		private System.Nullable<int> _Mesi;
		
		private System.Nullable<int> _primaUscita;
		
		private System.Nullable<int> _secondaUscita;
		
		private System.Nullable<int> _terzaUscita;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdTipologiaChanging(int value);
    partial void OnIdTipologiaChanged();
    partial void OnNomeChanging(string value);
    partial void OnNomeChanged();
    partial void OnGiorniChanging(System.Nullable<int> value);
    partial void OnGiorniChanged();
    partial void OnMesiChanging(System.Nullable<int> value);
    partial void OnMesiChanged();
    partial void OnprimaUscitaChanging(System.Nullable<int> value);
    partial void OnprimaUscitaChanged();
    partial void OnsecondaUscitaChanging(System.Nullable<int> value);
    partial void OnsecondaUscitaChanged();
    partial void OnterzaUscitaChanging(System.Nullable<int> value);
    partial void OnterzaUscitaChanged();
    #endregion
		
		public Tipologia()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdTipologia", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdTipologia
		{
			get
			{
				return this._IdTipologia;
			}
			set
			{
				if ((this._IdTipologia != value))
				{
					this.OnIdTipologiaChanging(value);
					this.SendPropertyChanging();
					this._IdTipologia = value;
					this.SendPropertyChanged("IdTipologia");
					this.OnIdTipologiaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nome", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Nome
		{
			get
			{
				return this._Nome;
			}
			set
			{
				if ((this._Nome != value))
				{
					this.OnNomeChanging(value);
					this.SendPropertyChanging();
					this._Nome = value;
					this.SendPropertyChanged("Nome");
					this.OnNomeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Giorni", DbType="Int")]
		public System.Nullable<int> Giorni
		{
			get
			{
				return this._Giorni;
			}
			set
			{
				if ((this._Giorni != value))
				{
					this.OnGiorniChanging(value);
					this.SendPropertyChanging();
					this._Giorni = value;
					this.SendPropertyChanged("Giorni");
					this.OnGiorniChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Mesi", DbType="Int")]
		public System.Nullable<int> Mesi
		{
			get
			{
				return this._Mesi;
			}
			set
			{
				if ((this._Mesi != value))
				{
					this.OnMesiChanging(value);
					this.SendPropertyChanging();
					this._Mesi = value;
					this.SendPropertyChanged("Mesi");
					this.OnMesiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_primaUscita", DbType="Int")]
		public System.Nullable<int> primaUscita
		{
			get
			{
				return this._primaUscita;
			}
			set
			{
				if ((this._primaUscita != value))
				{
					this.OnprimaUscitaChanging(value);
					this.SendPropertyChanging();
					this._primaUscita = value;
					this.SendPropertyChanged("primaUscita");
					this.OnprimaUscitaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_secondaUscita", DbType="Int")]
		public System.Nullable<int> secondaUscita
		{
			get
			{
				return this._secondaUscita;
			}
			set
			{
				if ((this._secondaUscita != value))
				{
					this.OnsecondaUscitaChanging(value);
					this.SendPropertyChanging();
					this._secondaUscita = value;
					this.SendPropertyChanged("secondaUscita");
					this.OnsecondaUscitaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_terzaUscita", DbType="Int")]
		public System.Nullable<int> terzaUscita
		{
			get
			{
				return this._terzaUscita;
			}
			set
			{
				if ((this._terzaUscita != value))
				{
					this.OnterzaUscitaChanging(value);
					this.SendPropertyChanging();
					this._terzaUscita = value;
					this.SendPropertyChanged("terzaUscita");
					this.OnterzaUscitaChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Periodico")]
	public partial class Periodico : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdPeriodico;
		
		private string _Nome;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdPeriodicoChanging(int value);
    partial void OnIdPeriodicoChanged();
    partial void OnNomeChanging(string value);
    partial void OnNomeChanged();
    #endregion
		
		public Periodico()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdPeriodico", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdPeriodico
		{
			get
			{
				return this._IdPeriodico;
			}
			set
			{
				if ((this._IdPeriodico != value))
				{
					this.OnIdPeriodicoChanging(value);
					this.SendPropertyChanging();
					this._IdPeriodico = value;
					this.SendPropertyChanged("IdPeriodico");
					this.OnIdPeriodicoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nome", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Nome
		{
			get
			{
				return this._Nome;
			}
			set
			{
				if ((this._Nome != value))
				{
					this.OnNomeChanging(value);
					this.SendPropertyChanging();
					this._Nome = value;
					this.SendPropertyChanged("Nome");
					this.OnNomeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Cronologia")]
	public partial class Cronologia : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdPeriodico;
		
		private int _IdMagazine;
		
		private System.DateTime _Data;
		
		private int _NumeroMagazineVenduti;
		
		private int _NumeroMagazineResi;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdPeriodicoChanging(int value);
    partial void OnIdPeriodicoChanged();
    partial void OnIdMagazineChanging(int value);
    partial void OnIdMagazineChanged();
    partial void OnDataChanging(System.DateTime value);
    partial void OnDataChanged();
    partial void OnNumeroMagazineVendutiChanging(int value);
    partial void OnNumeroMagazineVendutiChanged();
    partial void OnNumeroMagazineResiChanging(int value);
    partial void OnNumeroMagazineResiChanged();
    #endregion
		
		public Cronologia()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdPeriodico", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int IdPeriodico
		{
			get
			{
				return this._IdPeriodico;
			}
			set
			{
				if ((this._IdPeriodico != value))
				{
					this.OnIdPeriodicoChanging(value);
					this.SendPropertyChanging();
					this._IdPeriodico = value;
					this.SendPropertyChanged("IdPeriodico");
					this.OnIdPeriodicoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdMagazine", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int IdMagazine
		{
			get
			{
				return this._IdMagazine;
			}
			set
			{
				if ((this._IdMagazine != value))
				{
					this.OnIdMagazineChanging(value);
					this.SendPropertyChanging();
					this._IdMagazine = value;
					this.SendPropertyChanged("IdMagazine");
					this.OnIdMagazineChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Data", DbType="DateTime NOT NULL", IsPrimaryKey=true)]
		public System.DateTime Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				if ((this._Data != value))
				{
					this.OnDataChanging(value);
					this.SendPropertyChanging();
					this._Data = value;
					this.SendPropertyChanged("Data");
					this.OnDataChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroMagazineVenduti", DbType="Int NOT NULL")]
		public int NumeroMagazineVenduti
		{
			get
			{
				return this._NumeroMagazineVenduti;
			}
			set
			{
				if ((this._NumeroMagazineVenduti != value))
				{
					this.OnNumeroMagazineVendutiChanging(value);
					this.SendPropertyChanging();
					this._NumeroMagazineVenduti = value;
					this.SendPropertyChanged("NumeroMagazineVenduti");
					this.OnNumeroMagazineVendutiChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroMagazineResi", DbType="Int NOT NULL")]
		public int NumeroMagazineResi
		{
			get
			{
				return this._NumeroMagazineResi;
			}
			set
			{
				if ((this._NumeroMagazineResi != value))
				{
					this.OnNumeroMagazineResiChanging(value);
					this.SendPropertyChanging();
					this._NumeroMagazineResi = value;
					this.SendPropertyChanged("NumeroMagazineResi");
					this.OnNumeroMagazineResiChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Magazine")]
	public partial class Magazine : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _IdMagazine;
		
		private int _IdPeriodico;
		
		private int _NumeroCopieTotale;
		
		private int _NumeroCopieVendute;
		
		private int _NumeroCopieRese;
		
		private System.Nullable<int> _Numero;
		
		private string _Nome;
		
		private System.DateTime _DataDiConsegna;
		
		private System.DateTime _DataDiReso;
		
		private decimal _Prezzo;
		
		private int _IdTipologia;
		
		private string _ISSN;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdMagazineChanging(int value);
    partial void OnIdMagazineChanged();
    partial void OnIdPeriodicoChanging(int value);
    partial void OnIdPeriodicoChanged();
    partial void OnNumeroCopieTotaleChanging(int value);
    partial void OnNumeroCopieTotaleChanged();
    partial void OnNumeroCopieVenduteChanging(int value);
    partial void OnNumeroCopieVenduteChanged();
    partial void OnNumeroCopieReseChanging(int value);
    partial void OnNumeroCopieReseChanged();
    partial void OnNumeroChanging(System.Nullable<int> value);
    partial void OnNumeroChanged();
    partial void OnNomeChanging(string value);
    partial void OnNomeChanged();
    partial void OnDataDiConsegnaChanging(System.DateTime value);
    partial void OnDataDiConsegnaChanged();
    partial void OnDataDiResoChanging(System.DateTime value);
    partial void OnDataDiResoChanged();
    partial void OnPrezzoChanging(decimal value);
    partial void OnPrezzoChanged();
    partial void OnIdTipologiaChanging(int value);
    partial void OnIdTipologiaChanged();
    partial void OnISSNChanging(string value);
    partial void OnISSNChanged();
    #endregion
		
		public Magazine()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdMagazine", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int IdMagazine
		{
			get
			{
				return this._IdMagazine;
			}
			set
			{
				if ((this._IdMagazine != value))
				{
					this.OnIdMagazineChanging(value);
					this.SendPropertyChanging();
					this._IdMagazine = value;
					this.SendPropertyChanged("IdMagazine");
					this.OnIdMagazineChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdPeriodico", DbType="Int NOT NULL")]
		public int IdPeriodico
		{
			get
			{
				return this._IdPeriodico;
			}
			set
			{
				if ((this._IdPeriodico != value))
				{
					this.OnIdPeriodicoChanging(value);
					this.SendPropertyChanging();
					this._IdPeriodico = value;
					this.SendPropertyChanged("IdPeriodico");
					this.OnIdPeriodicoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroCopieTotale", DbType="Int NOT NULL")]
		public int NumeroCopieTotale
		{
			get
			{
				return this._NumeroCopieTotale;
			}
			set
			{
				if ((this._NumeroCopieTotale != value))
				{
					this.OnNumeroCopieTotaleChanging(value);
					this.SendPropertyChanging();
					this._NumeroCopieTotale = value;
					this.SendPropertyChanged("NumeroCopieTotale");
					this.OnNumeroCopieTotaleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroCopieVendute", DbType="Int NOT NULL")]
		public int NumeroCopieVendute
		{
			get
			{
				return this._NumeroCopieVendute;
			}
			set
			{
				if ((this._NumeroCopieVendute != value))
				{
					this.OnNumeroCopieVenduteChanging(value);
					this.SendPropertyChanging();
					this._NumeroCopieVendute = value;
					this.SendPropertyChanged("NumeroCopieVendute");
					this.OnNumeroCopieVenduteChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroCopieRese", DbType="Int NOT NULL")]
		public int NumeroCopieRese
		{
			get
			{
				return this._NumeroCopieRese;
			}
			set
			{
				if ((this._NumeroCopieRese != value))
				{
					this.OnNumeroCopieReseChanging(value);
					this.SendPropertyChanging();
					this._NumeroCopieRese = value;
					this.SendPropertyChanged("NumeroCopieRese");
					this.OnNumeroCopieReseChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Numero", DbType="Int")]
		public System.Nullable<int> Numero
		{
			get
			{
				return this._Numero;
			}
			set
			{
				if ((this._Numero != value))
				{
					this.OnNumeroChanging(value);
					this.SendPropertyChanging();
					this._Numero = value;
					this.SendPropertyChanged("Numero");
					this.OnNumeroChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nome", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Nome
		{
			get
			{
				return this._Nome;
			}
			set
			{
				if ((this._Nome != value))
				{
					this.OnNomeChanging(value);
					this.SendPropertyChanging();
					this._Nome = value;
					this.SendPropertyChanged("Nome");
					this.OnNomeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DataDiConsegna", DbType="DateTime NOT NULL")]
		public System.DateTime DataDiConsegna
		{
			get
			{
				return this._DataDiConsegna;
			}
			set
			{
				if ((this._DataDiConsegna != value))
				{
					this.OnDataDiConsegnaChanging(value);
					this.SendPropertyChanging();
					this._DataDiConsegna = value;
					this.SendPropertyChanged("DataDiConsegna");
					this.OnDataDiConsegnaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DataDiReso", DbType="DateTime NOT NULL")]
		public System.DateTime DataDiReso
		{
			get
			{
				return this._DataDiReso;
			}
			set
			{
				if ((this._DataDiReso != value))
				{
					this.OnDataDiResoChanging(value);
					this.SendPropertyChanging();
					this._DataDiReso = value;
					this.SendPropertyChanged("DataDiReso");
					this.OnDataDiResoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Prezzo", DbType="Decimal(9,2) NOT NULL")]
		public decimal Prezzo
		{
			get
			{
				return this._Prezzo;
			}
			set
			{
				if ((this._Prezzo != value))
				{
					this.OnPrezzoChanging(value);
					this.SendPropertyChanging();
					this._Prezzo = value;
					this.SendPropertyChanged("Prezzo");
					this.OnPrezzoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IdTipologia", DbType="Int NOT NULL")]
		public int IdTipologia
		{
			get
			{
				return this._IdTipologia;
			}
			set
			{
				if ((this._IdTipologia != value))
				{
					this.OnIdTipologiaChanging(value);
					this.SendPropertyChanging();
					this._IdTipologia = value;
					this.SendPropertyChanged("IdTipologia");
					this.OnIdTipologiaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ISSN", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ISSN
		{
			get
			{
				return this._ISSN;
			}
			set
			{
				if ((this._ISSN != value))
				{
					this.OnISSNChanging(value);
					this.SendPropertyChanging();
					this._ISSN = value;
					this.SendPropertyChanged("ISSN");
					this.OnISSNChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Dashboard")]
	public partial class Dashboard
	{
		
		private string _ISSN;
		
		private string _Periodico;
		
		private string _Rivista;
		
		private System.Nullable<int> _Numero;
		
		private decimal _Prezzo;
		
		private System.Nullable<int> _Copie_presenti;
		
		private System.DateTime _Data_di_consegna;
		
		private System.DateTime _Data_di_reso;
		
		private string _Tipologia;
		
		public Dashboard()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ISSN", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ISSN
		{
			get
			{
				return this._ISSN;
			}
			set
			{
				if ((this._ISSN != value))
				{
					this._ISSN = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Periodico", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Periodico
		{
			get
			{
				return this._Periodico;
			}
			set
			{
				if ((this._Periodico != value))
				{
					this._Periodico = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Rivista", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Rivista
		{
			get
			{
				return this._Rivista;
			}
			set
			{
				if ((this._Rivista != value))
				{
					this._Rivista = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Numero", DbType="Int")]
		public System.Nullable<int> Numero
		{
			get
			{
				return this._Numero;
			}
			set
			{
				if ((this._Numero != value))
				{
					this._Numero = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Prezzo", DbType="Decimal(9,2) NOT NULL")]
		public decimal Prezzo
		{
			get
			{
				return this._Prezzo;
			}
			set
			{
				if ((this._Prezzo != value))
				{
					this._Prezzo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Copie presenti]", Storage="_Copie_presenti", DbType="Int")]
		public System.Nullable<int> Copie_presenti
		{
			get
			{
				return this._Copie_presenti;
			}
			set
			{
				if ((this._Copie_presenti != value))
				{
					this._Copie_presenti = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Data di consegna]", Storage="_Data_di_consegna", DbType="DateTime NOT NULL")]
		public System.DateTime Data_di_consegna
		{
			get
			{
				return this._Data_di_consegna;
			}
			set
			{
				if ((this._Data_di_consegna != value))
				{
					this._Data_di_consegna = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Data di reso]", Storage="_Data_di_reso", DbType="DateTime NOT NULL")]
		public System.DateTime Data_di_reso
		{
			get
			{
				return this._Data_di_reso;
			}
			set
			{
				if ((this._Data_di_reso != value))
				{
					this._Data_di_reso = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tipologia", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Tipologia
		{
			get
			{
				return this._Tipologia;
			}
			set
			{
				if ((this._Tipologia != value))
				{
					this._Tipologia = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.ViewHistory")]
	public partial class ViewHistory
	{
		
		private string _ISSN;
		
		private string _Periodico;
		
		private string _Magazine;
		
		private System.Nullable<int> _Numero;
		
		private string _Tipologia;
		
		private System.DateTime _Data;
		
		private int _NumeroMagazineResi;
		
		private int _NumeroMagazineVenduti;
		
		private decimal _Prezzo;
		
		private System.Nullable<decimal> _Prezzo_Totale;
		
		public ViewHistory()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ISSN", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ISSN
		{
			get
			{
				return this._ISSN;
			}
			set
			{
				if ((this._ISSN != value))
				{
					this._ISSN = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Periodico", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Periodico
		{
			get
			{
				return this._Periodico;
			}
			set
			{
				if ((this._Periodico != value))
				{
					this._Periodico = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Magazine", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Magazine
		{
			get
			{
				return this._Magazine;
			}
			set
			{
				if ((this._Magazine != value))
				{
					this._Magazine = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Numero", DbType="Int")]
		public System.Nullable<int> Numero
		{
			get
			{
				return this._Numero;
			}
			set
			{
				if ((this._Numero != value))
				{
					this._Numero = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tipologia", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Tipologia
		{
			get
			{
				return this._Tipologia;
			}
			set
			{
				if ((this._Tipologia != value))
				{
					this._Tipologia = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Data", DbType="DateTime NOT NULL")]
		public System.DateTime Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				if ((this._Data != value))
				{
					this._Data = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroMagazineResi", DbType="Int NOT NULL")]
		public int NumeroMagazineResi
		{
			get
			{
				return this._NumeroMagazineResi;
			}
			set
			{
				if ((this._NumeroMagazineResi != value))
				{
					this._NumeroMagazineResi = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroMagazineVenduti", DbType="Int NOT NULL")]
		public int NumeroMagazineVenduti
		{
			get
			{
				return this._NumeroMagazineVenduti;
			}
			set
			{
				if ((this._NumeroMagazineVenduti != value))
				{
					this._NumeroMagazineVenduti = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Prezzo", DbType="Decimal(9,2) NOT NULL")]
		public decimal Prezzo
		{
			get
			{
				return this._Prezzo;
			}
			set
			{
				if ((this._Prezzo != value))
				{
					this._Prezzo = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Prezzo Totale]", Storage="_Prezzo_Totale", DbType="Decimal(20,2)")]
		public System.Nullable<decimal> Prezzo_Totale
		{
			get
			{
				return this._Prezzo_Totale;
			}
			set
			{
				if ((this._Prezzo_Totale != value))
				{
					this._Prezzo_Totale = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
