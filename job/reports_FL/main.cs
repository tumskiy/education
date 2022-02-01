var worksheet = WorkbookNonExcel.Worksheets.FirstOrDefault();
// массив зафисированных
TariffZoneBasedParameter[] parameters=new TariffZoneBasedParameter[1];
parameters[0]=TariffZoneBasedParameter.Instances.EnergyActiveForwardTotalFixDay;

var aplus = TariffZoneBasedParameter.Instances.EnergyActiveForwardTotalFixDay;
var aminus = TariffZoneBasedParameter.Instances.EnergyActiveReverseTotalFixDay;


//объединение 
var prms=parameters.Concat(TariffZoneBasedParameter.GetInstances().Where(x => x.AttributeClonedFrom != null).Where(x => parameters.Contains(x.AttributeClonedFrom))).ToArray(); 
TariffZoneBasedParameter[] parameters2=new TariffZoneBasedParameter[1];
parameters[0]=TariffZoneBasedParameter.Instances.EnergyActiveReverseTotalFixDay;
//объединение 
var prms2=parameters.Concat(TariffZoneBasedParameter.GetInstances().Where(x => x.AttributeClonedFrom != null).Where(x => parameters.Contains(x.AttributeClonedFrom))).ToArray(); 
//даты
var sd=ReportParams.sd;
var ed=ReportParams.ed;
if(sd==ed) sd.AddMonths(-1);
if(sd>ed)
{
	var td=sd;
	sd=ed;
	ed=sd;
}
DayIntervalData dInterval=new DayIntervalData{StartDt=sd,EndDt=ed}; 
DayIntervalData dInterval2=new DayIntervalData{StartDt=sd.AddDays(-1),EndDt=ed}; 
worksheet.Cells[5,0].Value = string.Format("Отчет о потреблении электроэнергии за период {0:dd.MM.yyyy} г. по {1:dd.MM.yyyy} г.", sd, ed);

//множество ТУ
var mpList=new HashSet<MeterPoint>();
//Классификатор
if(ReportParams.ClassifierNodesFilterDetected.HasValue && ReportParams.ClassifierNodesFilterDetected.Value)
{
    var cls=ReportParams.ClassifierNodes.ToArray();
    foreach(var cl in cls)
    {
    var mp=cl as MeterPoint;
    if(mp!=null) mpList.Add(mp);
    }
}
else
{
    mpList = ReportParams.ClassifierNodes.SelectMany(x =>x.GetAllChildrenOfClass(MeterPoint.GetClassInfo())).OfType<MeterPoint>().Distinct().ToHashSet();
}


int stRow = 8; 	// начальная строка
int indexNumber = 1;
int col = 0;
int j = 0;
//using для ускорения работы
using (PreloadManager.Current.RegisterCache(() => new MeterPointGetEnergyValueCache(mpList,prms,dInterval)))
{	
//цикл по ТУ, пока они есть в массиве
	foreach(var meterPoint in mpList)
	{	
		var el = meterPoint.AttributeElectricityMeter;
		if (el != null)
		{
			var arr=new double?[8];
			// № п/п
			worksheet.Cells[stRow, col+0].Value = indexNumber;
			// № кв.
			worksheet.Cells[stRow, col+1].Value = meterPoint.AttributeCaption.TryGetFlatNum();
			// Лицевой счет Аб+
			var consumers = meterPoint.AttributeConsumer.ToArray();
			if(consumers.Length != 0)
			{
				List<string> consumerList = new List<string>();
				foreach(var consumer in consumers)
				{
					consumerList.Add(consumer.AttributeCurrentAccount);	
				}
				worksheet.Cells[stRow, col+2].Value = string.Join("/", consumerList);
			}
			
			// Адрес
			worksheet.Cells[stRow, col+3].Value = meterPoint.AttributeAddress;
			
			//номер
			worksheet.Cells[stRow, col+4].Value = el.AttributeSerialNumber;
			
			//Дата установки
			worksheet.Cells[stRow, col+5].Value = el.AttributeInstallDate;
			
			/*    В работе в данный момент
			///_________ПОДСЧЕТ ДЕЛЬТЫ ПО А+______________________
			var dataDeltaPlus = meterPoint.GetMeterPointFinalData(aplus, dInterval);
			var startDataAplus = dataDeltaPlus.FirstOrDefault(x=>x.ValueDt != null && x.ValueDt == sd);
			var endDataAplus = dataDeltaPlus.FirstOrDefault(x=>x.ValueDt != null && x.ValueDt == ed);
			
			//var deltaAplus = startDataAplus.Value - endDataAplus.Value;
			worksheet.Cells[stRow, col+4+15].Value = startDataAplus.Value;
			worksheet.Cells[stRow, col+4+16].Value = endDataAplus.Value;
			
			///_________ПОДСЧЕТ ДЕЛЬТЫ ПО А-______________________
			var dataDeltaMinus = meterPoint.GetMeterPointFinalData(aminus, dInterval);
			var startDataAminus = dataDeltaMinus.FirstOrDefault(x=>x.ValueDt != null && x.ValueDt == sd);
			var endDataAminus = dataDeltaMinus.FirstOrDefault(x=>x.ValueDt != null && x.ValueDt == ed);
			
			//var deltaAminus = startDataAminus.Value - endDataAminus.Value;
			//worksheet.Cells[stRow, col+5].Value = deltaAminus;
			worksheet.Cells[stRow, col+4+15].Value = startDataAminus.Value;
			worksheet.Cells[stRow, col+4+16].Value = endDataAminus.Value;
			
			*/
			
			//словари 1 - А+, 2 - А-
			Dictionary <DateTime, Tuple <double?,double?,double?,double?>> dic=null; // <double?,double?,double?,double?> - здесь. 1элемент - началао суток, 2ой - тариф 1, 2-3й тариф и тд
			Dictionary <DateTime, Tuple <double?,double?,double?,double?>> dic2=null;
			if(meterPoint.AttributeTariff!=null)
			{
				dic = Helpers.getDataToDictionary(meterPoint,prms,dInterval);
				dic2 = Helpers.getDataToDictionary(meterPoint,prms2,dInterval);
			}
			//цикл параметров				
			// работа с А+
			if(dic!=null && dic.Any())
				{	//сортировка по последним занчениям в словаре
					var order_dic = dic.OrderBy(x=>x.Key);			
					foreach (var val in order_dic)
						{
							//заполнение даты
							worksheet.Cells[stRow, col+5+4].SetValue(val.Key);
							worksheet.Cells[stRow, col+5+4].Style.NumberFormat="dd.mm.yyyy hh:mm:ss";
							//проверка на null и заполнение ячеек
							if(val.Value.Item1 != null)
							worksheet.Cells[stRow, col+4+4].Value = val.Value.Item1.Value;
							if(val.Value.Item2 != null)
							worksheet.Cells[stRow, col+6+4].Value = val.Value.Item2.Value;
							if(val.Value.Item3 != null)
							worksheet.Cells[stRow, col+7+4].Value = val.Value.Item3.Value;
							if(val.Value.Item4 != null)
							worksheet.Cells[stRow, col+8+4].Value = val.Value.Item4.Value;
							
						}
				}
				// работа с А-
				if(dic2!=null && dic2.Any())
				{	//сортировка по последним занчениям в словаре
					var order_dic = dic2.OrderBy(x=>x.Key);				
					foreach (var val in order_dic)
						{			
							//проверка на null и заполнение ячеек
							if(val.Value.Item1 != null)
							worksheet.Cells[stRow, col+9+4].Value = val.Value.Item1.Value;
							if(val.Value.Item2 != null)
							worksheet.Cells[stRow, col+10+4].Value = val.Value.Item2.Value;
							if(val.Value.Item3 != null)
							worksheet.Cells[stRow, col+11+4].Value = val.Value.Item3.Value;
							if(val.Value.Item4 != null)
							worksheet.Cells[stRow, col+12+4].Value = val.Value.Item4.Value;
							
						}
				}
				
				// Коэффициент
				var ratio = meterPoint.GetMeasureTransformersInfo();
				var ktt = ratio == null ? 1.0 : ratio.VoltageRatio.GetValueOrDefault(1.0);
				var ktn = ratio == null ? 1.0 : ratio.CurrentRatio.GetValueOrDefault(1.0);
				worksheet.Cells[stRow, col+13+4].Value = (ktn+"/"+ktt);
				
				stRow++;
				indexNumber++;
				j++;
							
		}	
	}
}

var dataCells = worksheet.Cells.GetSubrangeAbsolute(stRow-j,col,stRow-1,col+13);
dataCells.SetBorder();	
dataCells.Style.WrapText = true;
dataCells.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
dataCells.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

	
	
