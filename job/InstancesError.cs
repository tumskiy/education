
var workSheet = WorkbookNonExcel.Worksheets.FirstOrDefault();

WorkbookNonExcel.Worksheets.Add("Данные с kt");
var workSheetkt = WorkbookNonExcel.Worksheets[1];

// выравнивание дат
ReportParams.Id2 = ReportParams.Id2.AlignToHour();
ReportParams.Id3 = ReportParams.Id3.AlignToHour();
if (!string.IsNullOrEmpty(ReportFileName))
	workSheet.Cells[0, 0].Value = ReportFileName;
	workSheetkt.Cells[0, 0].Value = ReportFileName;
	workSheet.Cells[1, 0].Value = string.Format("За период c {0:dd.MM.yyyy} г. по {1:dd.MM.yyyy} г.", ReportParams.Id2, ReportParams.Id3);
	workSheet.Cells[2, 0].Value = string.Format("Сформирован: {0:dd.MM.yyyy HH:mm}", 
	DateTime.UtcNow.AddHours(User.GetClassInfo().GetCurrentUserTimeZoneOffset()));
	workSheetkt.Cells[1, 0].Value = string.Format("За период c {0:dd.MM.yyyy} г. по {1:dd.MM.yyyy} г.", ReportParams.Id2, ReportParams.Id3);
	workSheetkt.Cells[2, 0].Value = string.Format("Сформирован: {0:dd.MM.yyyy HH:mm}", 
	DateTime.UtcNow.AddHours(User.GetClassInfo().GetCurrentUserTimeZoneOffset()));
	//workSheet.Cells[5, 9].Value = string.Format("{0:dd.MM.yyyy}", ReportParams.Id2);
	//workSheet.Cells[5, 10].Value = string.Format("{0:dd.MM.yyyy}", ReportParams.Id3);
	var meterPoints = ReportParams.Id1.SelectMany(x => x.GetAllChildrenOfClass(MeterPoint.GetClassInfo())).OfType<MeterPoint>().Distinct().ToArray();
	var stationname=ReportParams.Id1.FirstOrDefault().Caption;
	ReportFileName=ReportFileName+stationname;
	var interval=new DayIntervalData();
	interval.StartDt=ReportParams.Id2;
	interval.EndDt=ReportParams.Id3;
			
			// исходные данные по параметрам				
var sourceParameters = new List<Parameter>
{
	ThreePhaseParameter.Instances.ActivePowerPhase1,
	ThreePhaseParameter.Instances.ActivePowerPhase2,
	ThreePhaseParameter.Instances.ActivePowerPhase3,
	//------------------------------------------
	ThreePhaseParameter.Instances.ActivePowerSummary,
	//------------------------------------------
	ThreePhaseParameter.Instances.ReactivePowerPhase1,
	ThreePhaseParameter.Instances.ReactivePowerPhase2,
	ThreePhaseParameter.Instances.ReactivePowerPhase3,
	ThreePhaseParameter.Instances.CurrentPhase1,
	ThreePhaseParameter.Instances.CurrentPhase2,
	ThreePhaseParameter.Instances.CurrentPhase3,
	ThreePhaseParameter.Instances.VoltagePhase1,
	ThreePhaseParameter.Instances.VoltagePhase2,
	ThreePhaseParameter.Instances.VoltagePhase3,
	//------------------------------------------------
	ThreePhaseParameter.Instances.PowerFactorPhase1,
	ThreePhaseParameter.Instances.PowerFactorPhase2,
	ThreePhaseParameter.Instances.PowerFactorPhase3,
	//------------------------------------------
	ThreePhaseParameter.Instances.VoltageCurrentAnglePhase1,
	ThreePhaseParameter.Instances.VoltageCurrentAnglePhase2,
	ThreePhaseParameter.Instances.VoltageCurrentAnglePhase3,
	//------------------------------------------
	InterphaseBasedParameter.Instances.VoltageAngle12,
	InterphaseBasedParameter.Instances.VoltageAngle23,
	InterphaseBasedParameter.Instances.VoltageAngle31
	
};
	var parametersForSearch = new HashSet<Parameter>(sourceParameters);
	
	// размеры таблицы и т.п.
	var tableColumnsCount = 23;
	var tableStartRow = 4;
	var dataStartColumn = 1;
	var meterPointIndex = 0;
	
	
	var firstDataColumn=5;
	
	using (PreloadManager.Current.RegisterCache(() => new DataSourceReceiveDataBatchCache(meterPoints, parametersForSearch, new DayIntervalData{StartDt = ReportParams.Id2.AddSeconds(-10), EndDt = ReportParams.Id3.AddSeconds(10)})));
	var i=1;
	workSheet.Cells[0,0].Value= meterPoints.Count();
	Action<int, int> paintCellRed = (int x, int y) => workSheet.Cells[tableStartRow+x, dataStartColumn+y + tableStartRow].Style.FillPattern.SetSolid(Color.FromArgb(0, 255, 100, 100));
	Action<int, int> paintCellGreen = (int x, int y) => workSheet.Cells[tableStartRow+x, dataStartColumn+y + tableStartRow].Style.FillPattern.SetSolid(Color.FromArgb(0, 0, 255, 55));
	Action<int, int> paintCellYellow = (int x, int y) => workSheet.Cells[tableStartRow+x, dataStartColumn+ y + tableStartRow].Style.FillPattern.SetSolid(Color.FromArgb(0, 255, 255, 0));
try
{

	workSheet.Cells[tableStartRow, dataStartColumn+2].Value="Ктт";
	workSheetkt.Cells[tableStartRow, dataStartColumn+2].Value="Ктт";
	workSheet.Cells[tableStartRow, dataStartColumn+3].Value="Ктн";
	workSheetkt.Cells[tableStartRow, dataStartColumn+3].Value="Ктн";
	workSheet.Cells[tableStartRow, dataStartColumn+1].Value="Заводской номер";
	workSheetkt.Cells[tableStartRow, dataStartColumn+1].Value="Заводской номер";
	workSheet.Cells[tableStartRow, dataStartColumn].Value="Наименование точки учета";
	workSheetkt.Cells[tableStartRow, dataStartColumn].Value="Наименование точки учета";	

	foreach (MeterPoint mp in meterPoints)
	{	
		var arr=new double?[22];
		workSheet.Cells[i+4,0].Value = i;
		var elm=mp.AttributeElectricityMeter;
		workSheet.Cells[tableStartRow+i, dataStartColumn].Value=mp.Caption;
		workSheetkt.Cells[tableStartRow+i, dataStartColumn].Value=mp.Caption;
		
		workSheet.Cells[tableStartRow+i, firstDataColumn+23].Value=elm.Class.Caption;//тип ПУ
		
		if (elm!=null) 
		{
			workSheet.Cells[tableStartRow+i, dataStartColumn+1].Value=elm.AttributeSerialNumber;
			workSheetkt.Cells[tableStartRow+i, dataStartColumn+1].Value=elm.AttributeSerialNumber;
		}
			
		var transformersInfo = mp.GetMeasureTransformersInfo();
		var ktt = transformersInfo.CurrentRatio != null ? transformersInfo.CurrentRatio.Value : 1.0;
		var ktn = transformersInfo.VoltageRatio != null ? transformersInfo.VoltageRatio.Value : 1.0;
		var tzpkt=new[]{1000*ktt*ktn,1000*ktt*ktn,1000*ktt*ktn,1000*ktt*ktn,1000*ktt*ktn,1000*ktt*ktn,1000*ktt*ktn,ktt,ktt,ktt,ktn,ktn,ktn,1,1,1,1,1,1,1,1,1};// добавлено *, 1000*ktt*ktn, *,1,1,1
		var tzpktkt=new[]{1000,1000,1000,1000,1000,1000,1000,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};//добавлено: *, 1000, *,1,1,1
			////////////////////////////////////////////////////////////////		
		workSheet.Cells[tableStartRow+i, dataStartColumn+2].Value=ktt;
		workSheetkt.Cells[tableStartRow+i, dataStartColumn+2].Value=ktt;
		workSheet.Cells[tableStartRow+i, dataStartColumn+3].Value=ktn;
		workSheetkt.Cells[tableStartRow+i, dataStartColumn+3].Value=ktn;	
		
		
		var y=1;
		var z=1;
		
		
		var kt=ktt*ktn;
		int x=0;
		var errors = new List<string>();
		foreach (Parameter tzp in parametersForSearch)				
		{
			workSheet.Cells[4, 5+x].Value=tzp.Caption;
			var data=mp.GetMeterPointFinalData(tzp,interval).ToArray();
			if (data.Any())   //сортировка массива, что бы не ошибиться
			{
				data=data.OrderBy(x=>x.ValueDt).ToArray();
				arr[x]=data.LastOrDefault().Value;
			}
			x++;
		}
		x=4; //начинаем с поля 4
		foreach(var ar in arr)
		{
			x++;
			if(ar.HasValue) //пока в массиве есть данные
			{
				var k=1;
				if(x<11 || (x>15 && x<18)) k=1000; // ограничение на значения с использованием коэффициентов
				workSheet.Cells[tableStartRow+i, x].Value=ar.Value/k;	// деление значения полей на 1000 + кт 	
			}
			else 
				workSheet.Cells[tableStartRow+i, x].Value="----";
		}
		
		//
		
		//----------- Проверка отрицательной активки------------ НАЧАЛО
		//AddLogInfo($"data: {String.Concat(arr.Select(item => item + ", "))}");
		//AddLogInfo($"if1: {arr[1].HasValue} {(arr[1].HasValue ? arr[1].Value : 0)}");

		if (arr[0].HasValue && arr[0].Value<0) 	//проверка на отрицательную А	
		{
			paintCellRed(i, 0); // покрас
			errors.Add("отрицательная активная(a)");
			workSheet.Cells[tableStartRow+i, firstDataColumn+22].Value = string.Join(", ", errors); //вывод об ошибке
			
		}
		if (arr[1].HasValue && arr[1].Value<0)		
		{	
			errors.Add("отрицательная активная(b)");
			workSheet.Cells[tableStartRow+i, firstDataColumn+22].Value = string.Join(", ", errors);
			
		}
		if (arr[2].HasValue && arr[2].Value<0) 		
		{
			errors.Add("отрицательная активная(c)");
			workSheet.Cells[tableStartRow+i, firstDataColumn+22].Value = string.Join(", ", errors);
			
		}
		//----------- Проверка отрицательной активки------------КОНЕЦ
		
		
		
		//----------- РАсчет угла м/д векторами-----------------НАЧАЛО
		bool isAngleCorrect = true;
		if(arr[4].HasValue && arr[13].HasValue) // фаза а
		{
			var ang1 = (Math.Sign(arr[4].Value)*Math.Abs(Math.Acos(arr[13].Value))*180/Math.PI); //подсчет по формуле	
			workSheet.Cells[tableStartRow+i, firstDataColumn+16].Value=ang1; // запись в ячейку
			if(ang1 < -95 || ang1 > 95) //сравнение, если больше 90
			{
				paintCellRed(i, 16);
				isAngleCorrect = false;//сравнение на условие
			}
		}
		if(arr[5].HasValue && arr[14].HasValue) // фаза b 
		{
			var ang2 = (Math.Sign(arr[5].Value)*Math.Abs(Math.Acos(arr[14].Value))*180/Math.PI);//подсчет по формуле		
			workSheet.Cells[tableStartRow+i, firstDataColumn+17].Value=ang2;// запись в ячейку
			if(ang2 < -95 || ang2 > 95) //сравнение, если больше 90
			{
				paintCellRed(i, 17);
				isAngleCorrect = false; //сравнение на условие
			}
		}
		if(arr[6].HasValue && arr[15].HasValue) // фаза c
		{
			var ang3 = (Math.Sign(arr[6].Value)*Math.Abs(Math.Acos(arr[15].Value))*180/Math.PI);//подсчет по формуле		
			workSheet.Cells[tableStartRow+i, firstDataColumn+18].Value=ang3;// запись в ячейку
			if(ang3 < -95 || ang3 > 95) //сравнение, если больше 90
			{
				paintCellRed(i, 18);
				isAngleCorrect = false;//сравнение на условие
			}
		}
		if(!isAngleCorrect)
		{
			errors.Add("Угол между векторами не соответствует");
			workSheet.Cells[tableStartRow+i, firstDataColumn+22].Value = string.Join(", ", errors);
			
		}		
		//----------- РАсчет угла м/д векторами-----------------НАЧАЛО
		
		
		//----------- проверка на двухэлементную схему
		if(arr[8].HasValue && arr[8].Value == 0 && arr[7].HasValue && arr[7].Value != 0 && arr[9].HasValue && arr[9].Value != 0) //проверка на 2х элм.схему, если ток по ф. Б=0, а остальные нет
		{
		
			if(arr[19].HasValue && arr[20].HasValue && arr[21].HasValue) // если есть занчения
			{
				var UI12 = arr[19].Value; //получение значений
				var UI23 = arr[20].Value;
				var UI31 = arr[21].Value;
				if ((
					(UI12>10 && UI12<350 || UI23>350 && UI23<10 || UI31<50 && UI31>70) ||           ///переборка углов
					(UI12>10 && UI12<350 || UI23<280 && UI23>300 || UI31<60 && UI31>80) ||
					(UI12<350 && UI12>10 || UI23>310 && UI23 <290 || UI31>-50 && UI31<-70) ||
					(UI12>10 && UI12<350 || UI23<280 && UI23>300 || UI31>-80 && UI31 <-80)
				
				))
				{
					paintCellRed(i, 19);
					paintCellRed(i, 20);
					paintCellRed(i, 21);
					errors.Add("Угол между током и напряжением не соответствует");
					workSheet.Cells[tableStartRow+i, firstDataColumn+22].Value = string.Join(", ", errors);
				}
									
			}
		
		
		}
		else  
		{ //------ проверка на 3 элемента
			if(arr[19].HasValue && arr[20].HasValue && arr[21].HasValue){
				var UI12 = arr[19].Value;
				var UI23 = arr[20].Value;
				var UI31 = arr[21].Value;
				if(
					(UI12 < 115 && UI12 > 125 || UI23 < 115 && UI23 > 125 || UI31 < 115 && UI31 > 125 ) &&  ///переборка углов
					(UI12 < 355 && UI12 > 5 || UI23 < 115 && UI23 > 125 || UI31 < 235 && UI31 > 245 )
				)	
				{
					paintCellRed(i, 19);
					paintCellRed(i, 20);
					paintCellRed(i, 21);
					errors.Add("Угол между током и напряжением не соответствует");
					workSheet.Cells[tableStartRow+i, firstDataColumn+22].Value = string.Join(", ", errors);
				}
			}
		}
		
		
		//----------- Проверка суммарной активки------------ НАЧАЛО
		if(!arr[3].HasValue && arr[0].HasValue && arr[1].HasValue && arr[2].HasValue) //если данных нет
		{	
			var sum = arr[0].Value+arr[1].Value+arr[2].Value; // сложение в sum
			workSheet.Cells[tableStartRow+i, firstDataColumn+3].Value=sum/1000;	//конверция из КВт в Вт
			if (sum<0)
			{
				paintCellRed(i, 3);
			}
		}
		
		if(arr[3].HasValue){
			var sum = arr[3].Value;
			if(sum < 0)
			{
				paintCellRed(i, 3);	
				errors.Add("отрицательная суммарная активная");
				workSheet.Cells[tableStartRow+i, firstDataColumn+22].Value = string.Join(", ", errors);
				
			}
			if(sum == 0)
			{
				paintCellYellow(i, 3);
				errors.Add("нет нагрузки");
				workSheet.Cells[tableStartRow+i, firstDataColumn+22].Value = string.Join(", ", errors);
				
			}
		}
		
		//----------- Проверка суммарной активки------------КОНЕЦ		
		
		i++;
	}
}

	
catch (Exception e)
	{
		
	}

var range=workSheet.Cells.GetSubrangeAbsolute(tableStartRow,dataStartColumn,tableStartRow+i-1,dataStartColumn+tableColumnsCount+tableStartRow);
workSheet.Cells[dataStartColumn].Column.AutoFit();
workSheet.Cells[dataStartColumn+tableColumnsCount+tableStartRow].Column.AutoFit();
range.SetBorder();
range=workSheet.Cells.GetSubrangeAbsolute(tableStartRow,dataStartColumn,tableStartRow,dataStartColumn+tableColumnsCount+tableStartRow);
range.Style.WrapText=true;
var range1=workSheetkt.Cells.GetSubrangeAbsolute(tableStartRow,dataStartColumn,tableStartRow+i-1,dataStartColumn+tableColumnsCount+tableStartRow);
workSheetkt.Cells[dataStartColumn].Column.AutoFit();
workSheetkt.Cells[dataStartColumn+tableColumnsCount+tableStartRow].Column.AutoFit();
range1.SetBorder();
range1=workSheetkt.Cells.GetSubrangeAbsolute(tableStartRow,dataStartColumn,tableStartRow,dataStartColumn+tableColumnsCount+tableStartRow);
range1.Style.WrapText=true;
workSheet.Cells[0, 0].Value = ReportFileName;
workSheetkt.Cells[0, 0].Value = ReportFileName;

