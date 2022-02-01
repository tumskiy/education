public static class Helpers
{
	public static string TryGetFlatNum(this string value)
	{
		if (string.IsNullOrEmpty(value))
			return string.Empty;
		//Исходим из того, что AttributeCaption точки учета, формируется в ОЛ по следующему шаблону [Номер квартиры],Тип прибора учета, серийный номер. Где номера квартиры может и не быть
		var parts = value.Split(',');
		if (parts.Length > 2) return parts[0];
		return string.Empty;
	}
	 
	public static TariffZoneBasedParameter[] getTZParameters(TariffZoneBasedParameter[] parameters)
	{
		TariffZoneBasedParameter[] res=new TariffZoneBasedParameter[parameters.Count()];
		//res.Concat(parameters);
		foreach(var t in parameters)
		{
	    	res = res.Concat(TariffZoneBasedParameter.GetInstances().Where(x => x.AttributeClonedFrom == t)).ToArray();
	    }
		return res;
	}
	
public static  Dictionary <DateTime, Tuple <double?,double?,double?,double?>> getDataToDictionary(MeterPoint meterPoint, TariffZoneBasedParameter[] prms, DayIntervalData dInterval)
{	//словарь для 3х тарифных зон
	var dic=new Dictionary <DateTime, Tuple <double?,double?,double?,double?>> ();	
	var tariff = meterPoint.AttributeTariff;
	//if (tariff != null)
	{
		var tz1 = tariff.AttributeOrderedTariffZones.GetValues().Where(x => x.AttributeIndexInTariff != null && x.AttributeIndexInTariff == 1).Select(x => x.AttributeTariffZone).FirstOrDefault();
		var tz2 = tariff.AttributeOrderedTariffZones.GetValues().Where(x => x.AttributeIndexInTariff != null && x.AttributeIndexInTariff == 2).Select(x => x.AttributeTariffZone).FirstOrDefault();
		var tz3 = tariff.AttributeOrderedTariffZones.GetValues().Where(x => x.AttributeIndexInTariff != null && x.AttributeIndexInTariff == 3).Select(x => x.AttributeTariffZone).FirstOrDefault();
			
		foreach(var prm in prms)
		{
			
			//"ночная тарифная зона"
			//"пиковая тарифная зона"
			//"полупиковая тарифная зона"
			if (prm == prms[0])
				{
					var Get = meterPoint.GetMeterPointFinalData(prm, dInterval);	
					foreach (var DATA in Get)
					{	
						var math = DATA.Value/1000;
						if(!dic.ContainsKey(DATA.ValueDt.Value))
							{	
								var kor=new Tuple<double?,double?,double?,double?>(math, null, null, null); //<double?,double?,double?,double?> - здесь. 1элемент - началао суток, 2ой - тариф 1, 2-3й тариф и тд
								dic.Add(DATA.ValueDt.Value, kor);
							}
						 else
							{
								dic[DATA.ValueDt.Value] = new Tuple <double?,double?,double?,double?> 
								(math, dic[DATA.ValueDt.Value].Item1, dic[DATA.ValueDt.Value].Item2, dic[DATA.ValueDt.Value].Item3);
							}
					}
					
				}
			
			else if (prm.AttributeTariffZone == tz1 || prm.AttributeTariffZone == tz2 || prm.AttributeTariffZone == tz3)
					 {
					 	var Get = meterPoint.GetMeterPointFinalData(prm, dInterval); //получение последних данных
					 	foreach (var DATA in Get) 
					 	{
					 		var math = DATA.Value/1000; //КВт в Вт
					 		if(!dic.ContainsKey(DATA.ValueDt.Value))
						 		{
						 			var kor = new Tuple <double?, double?, double?, double?> (null, null, null, null);
						 			
						 			if(prm.AttributeTariffZone == tz1)																				// здесь переменная "math" сдвигается, в первом кортеже её нет, 
						 				kor = new Tuple <double?, double?, double?, double?> (null, math, null, null);								// во втором она на втором поле, это первый тариф,
						 																															// в третьем она сдвигается еще на элемент, во второй тариф
						 			if(prm.AttributeTariffZone == tz2)
						 				kor = new Tuple <double?, double?, double?, double?> (null, null, math, null);
						 			
						 			if(prm.AttributeTariffZone == tz3)
						 				kor = new Tuple <double?, double?, double?, double?> (null, null, null, math);
						 			dic.Add(DATA.ValueDt.Value, kor);
						 		}
					 		else 
					 			{	// заполненение 1го значения
					 				if (prm.AttributeTariffZone == tz1)
					 				dic[DATA.ValueDt.Value] = new Tuple <double?, double?, double?, double?>
					 				(dic[DATA.ValueDt.Value].Item1, math, dic[DATA.ValueDt.Value].Item3, dic[DATA.ValueDt.Value].Item4);
					 							 				
					 				// заполненение 2го значения
					 				if(prm.AttributeTariffZone == tz2)					 				
					 				dic[DATA.ValueDt.Value] = new Tuple <double?, double?, double?, double?>
					 				(dic[DATA.ValueDt.Value].Item1, dic[DATA.ValueDt.Value].Item2, math, dic[DATA.ValueDt.Value].Item4);
					 		
					 				// заполненение 3го значения
					 				if(prm.AttributeTariffZone == tz3)
					 				dic[DATA.ValueDt.Value] = new Tuple <double?, double?, double?, double?>
					 				(dic[DATA.ValueDt.Value].Item1, dic[DATA.ValueDt.Value].Item2, dic[DATA.ValueDt.Value].Item3, math);
					 			
					 			}
					 		
					 	}
					 }			
		}
		return dic;
	}
		
}



}
