/* Tags for Reactor 1 */
use EisaiReactorDB
go

/* Tags for End Block(Recipe Status, 'Reactor_6') */
	insert into dbo.FieldPoints values('RecipeStatus', 'RecipeStatus', 'FieldPoint', 'R6.RecipeStatus', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('RecipeEnded', 'RecipeEnded', 'FieldPoint', 'R6.RecipeEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('ClearRecipeStatus', 'ClearRecipeStatus', 'FieldPoint', 'R6.ClearRecipe', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('AbortRecipeStatus', 'AbortRecipeStatus', 'FieldPoint', 'R6.AbortRecipe', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('NumberOfRecipeSteps', 'NumberOfRecipeSteps','FieldPoint', 'R6.NumberOfRecipeSteps', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

/* Tags  for Start Block */
	insert into dbo.FieldPoints values('HeatCoolModeSelection_0', 'Recipe_Start_HeatCoolModeSelection_0', 'FieldPoint', 'R6.RecipeStep[0].HeatCoolModeSelection', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDeltaTemp_0', 'Recipe_Start_HeatCoolDeltaTemp_0', 'FieldPoint', 'R6.RecipeStep[0].HeatCoolDeltaTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('Started_0', 'Recipe_Start_Started_0', 'FieldPoint', 'R6.RecipeStep[0].Started', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('Ended_0', 'Recipe_Start_Ended_0', 'FieldPoint', 'R6.RecipeStep[0].Ended', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StartedTime_0', 'Recipe_Start_StartedTime_0', 'FieldPoint', 'R6.RecipeStep[0].StartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('EndedTime_0', 'Recipe_Start_EndedTime_0', 'FieldPoint', 'R6.RecipeStep[0].EndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

/* Tags for Heat Cool Block*/

	/* Tags for index 1 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_1', 'Recipe_HeatCool_Enabled_1', 'FieldPoint', 'R6.RecipeStep[1].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_1', 'Recipe_HeatCool_Started_1', 'FieldPoint', 'R6.RecipeStep[1].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_1', 'Recipe_HeatCool_Ended_1', 'FieldPoint', 'R6.RecipeStep[1].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
	insert into dbo.FieldPoints values('HeatCoolStartedTime_1', 'Recipe_HeatCool_StartedTime_1', 'FieldPoint', 'R6.RecipeStep[1].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEndedTime_1', 'Recipe_HeatCool_EndedTime_1', 'FieldPoint', 'R6.RecipeStep[1].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_1', 'Recipe_HeatCool_SetPoint_1', 'FieldPoint', 'R6.RecipeStep[1].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_1', 'Recipe_HeatCool_Duration_1', 'FieldPoint', 'R6.RecipeStep[1].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_1', 'Recipe_HeatCool_OperatingMode_1', 'FieldPoint', 'R6.RecipeStep[1].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 2 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_2', 'Recipe_HeatCool_Enabled_2', 'FieldPoint', 'R6.RecipeStep[2].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_2', 'Recipe_HeatCool_Started_2', 'FieldPoint', 'R6.RecipeStep[2].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_2', 'Recipe_HeatCool_Ended_2', 'FieldPoint', 'R6.RecipeStep[2].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
	insert into dbo.FieldPoints values('HeatCoolStartedTime_2', 'Recipe_HeatCool_StartedTime_2', 'FieldPoint', 'R6.RecipeStep[2].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEndedTime_2', 'Recipe_HeatCool_EndedTime_2', 'FieldPoint', 'R6.RecipeStep[2].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_2', 'Recipe_HeatCool_SetPoint_2', 'FieldPoint', 'R6.RecipeStep[2].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_2', 'Recipe_HeatCool_Duration_2', 'FieldPoint', 'R6.RecipeStep[2].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_2', 'Recipe_HeatCool_OperatingMode_2', 'FieldPoint', 'R6.RecipeStep[2].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 3 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_3', 'Recipe_HeatCool_Enabled_3', 'FieldPoint', 'R6.RecipeStep[3].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_3', 'Recipe_HeatCool_Started_3', 'FieldPoint', 'R6.RecipeStep[3].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_3', 'Recipe_HeatCool_Ended_3', 'FieldPoint', 'R6.RecipeStep[3].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
	insert into dbo.FieldPoints values('HeatCoolStartedTime_3', 'Recipe_HeatCool_StartedTime_3', 'FieldPoint', 'R6.RecipeStep[3].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEndedTime_3', 'Recipe_HeatCool_EndedTime_3', 'FieldPoint', 'R6.RecipeStep[3].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_3', 'Recipe_HeatCool_SetPoint_3', 'FieldPoint', 'R6.RecipeStep[3].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_3', 'Recipe_HeatCool_Duration_3', 'FieldPoint', 'R6.RecipeStep[3].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_3', 'Recipe_HeatCool_OperatingMode_3', 'FieldPoint', 'R6.RecipeStep[3].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 4 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_4', 'Recipe_HeatCool_Enabled_4', 'FieldPoint', 'R6.RecipeStep[4].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_4', 'Recipe_HeatCool_Started_4', 'FieldPoint', 'R6.RecipeStep[4].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_4', 'Recipe_HeatCool_Ended_4', 'FieldPoint', 'R6.RecipeStep[4].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
	insert into dbo.FieldPoints values('HeatCoolStartedTime_4', 'Recipe_HeatCool_StartedTime_4', 'FieldPoint', 'R6.RecipeStep[4].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEndedTime_4', 'Recipe_HeatCool_EndedTime_4', 'FieldPoint', 'R6.RecipeStep[4].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_4', 'Recipe_HeatCool_SetPoint_4', 'FieldPoint', 'R6.RecipeStep[4].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_4', 'Recipe_HeatCool_Duration_4', 'FieldPoint', 'R6.RecipeStep[4].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_4', 'Recipe_HeatCool_OperatingMode_4', 'FieldPoint', 'R6.RecipeStep[4].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 5 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_5', 'Recipe_HeatCool_Enabled_5', 'FieldPoint', 'R6.RecipeStep[5].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_5', 'Recipe_HeatCool_Started_5', 'FieldPoint', 'R6.RecipeStep[5].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_5', 'Recipe_HeatCool_Ended_5', 'FieldPoint', 'R6.RecipeStep[5].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
	insert into dbo.FieldPoints values('HeatCoolStartedTime_5', 'Recipe_HeatCool_StartedTime_5', 'FieldPoint', 'R6.RecipeStep[5].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEndedTime_5', 'Recipe_HeatCool_EndedTime_5', 'FieldPoint', 'R6.RecipeStep[5].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_5', 'Recipe_HeatCool_SetPoint_5', 'FieldPoint', 'R6.RecipeStep[5].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_5', 'Recipe_HeatCool_Duration_5', 'FieldPoint', 'R6.RecipeStep[5].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_5', 'Recipe_HeatCool_OperatingMode_5', 'FieldPoint', 'R6.RecipeStep[5].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 6 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_6', 'Recipe_HeatCool_Enabled_6', 'FieldPoint', 'R6.RecipeStep[6].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_6', 'Recipe_HeatCool_Started_6', 'FieldPoint', 'R6.RecipeStep[6].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_6', 'Recipe_HeatCool_Ended_6', 'FieldPoint', 'R6.RecipeStep[6].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('HeatCoolStartedTime_6', 'Recipe_HeatCool_StartedTime_6', 'FieldPoint', 'R6.RecipeStep[6].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('HeatCoolEndedTime_6', 'Recipe_HeatCool_EndedTime_6', 'FieldPoint', 'R6.RecipeStep[6].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_6', 'Recipe_HeatCool_SetPoint_6', 'FieldPoint', 'R6.RecipeStep[6].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_6', 'Recipe_HeatCool_Duration_6', 'FieldPoint', 'R6.RecipeStep[6].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_6', 'Recipe_HeatCool_OperatingMode_6', 'FieldPoint', 'R6.RecipeStep[6].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 7 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_7', 'Recipe_HeatCool_Enabled_7', 'FieldPoint', 'R6.RecipeStep[7].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_7', 'Recipe_HeatCool_Started_7', 'FieldPoint', 'R6.RecipeStep[7].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_7', 'Recipe_HeatCool_Ended_7', 'FieldPoint', 'R6.RecipeStep[7].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('HeatCoolStartedTime_7', 'Recipe_HeatCool_StartedTime_7', 'FieldPoint', 'R6.RecipeStep[7].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('HeatCoolEndedTime_7', 'Recipe_HeatCool_EndedTime_7', 'FieldPoint', 'R6.RecipeStep[7].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_7', 'Recipe_HeatCool_SetPoint_7', 'FieldPoint', 'R6.RecipeStep[7].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_7', 'Recipe_HeatCool_Duration_7', 'FieldPoint', 'R6.RecipeStep[7].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_7', 'Recipe_HeatCool_OperatingMode_7', 'FieldPoint', 'R6.RecipeStep[7].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')


	/* Tags for index 8 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_8', 'Recipe_HeatCool_Enabled_8', 'FieldPoint', 'R6.RecipeStep[8].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_8', 'Recipe_HeatCool_Started_8', 'FieldPoint', 'R6.RecipeStep[8].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_8', 'Recipe_HeatCool_Ended_8', 'FieldPoint', 'R6.RecipeStep[8].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('HeatCoolStartedTime_8', 'Recipe_HeatCool_StartedTime_8', 'FieldPoint', 'R6.RecipeStep[8].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('HeatCoolEndedTime_8', 'Recipe_HeatCool_EndedTime_8', 'FieldPoint', 'R6.RecipeStep[8].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_8', 'Recipe_HeatCool_SetPoint_8', 'FieldPoint', 'R6.RecipeStep[8].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_8', 'Recipe_HeatCool_Duration_8', 'FieldPoint', 'R6.RecipeStep[8].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_8', 'Recipe_HeatCool_OperatingMode_8', 'FieldPoint', 'R6.RecipeStep[8].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')


	/* Tags for index 9 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_9', 'Recipe_HeatCool_Enabled_9', 'FieldPoint', 'R6.RecipeStep[9].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_9', 'Recipe_HeatCool_Started_9', 'FieldPoint', 'R6.RecipeStep[9].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_9', 'Recipe_HeatCool_Ended_9', 'FieldPoint', 'R6.RecipeStep[9].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('HeatCoolStartedTime_9', 'Recipe_HeatCool_StartedTime_9', 'FieldPoint', 'R6.RecipeStep[9].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('HeatCoolEndedTime_9', 'Recipe_HeatCool_EndedTime_9', 'FieldPoint', 'R6.RecipeStep[9].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_9', 'Recipe_HeatCool_SetPoint_9', 'FieldPoint', 'R6.RecipeStep[9].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_9', 'Recipe_HeatCool_Duration_9', 'FieldPoint', 'R6.RecipeStep[9].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_9', 'Recipe_HeatCool_OperatingMode_9', 'FieldPoint', 'R6.RecipeStep[9].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')


	/* Tags for index 10 of array */
	insert into dbo.FieldPoints values('HeatCoolEnabled_10', 'Recipe_HeatCool_Enabled_10', 'FieldPoint', 'R6.RecipeStep[10].HeatCoolEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolStarted_10', 'Recipe_HeatCool_Started_10', 'FieldPoint', 'R6.RecipeStep[10].HeatCoolStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolEnded_10', 'Recipe_HeatCool_Ended_10', 'FieldPoint', 'R6.RecipeStep[10].HeatCoolEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('HeatCoolStartedTime_10', 'Recipe_HeatCool_StartedTime_10', 'FieldPoint', 'R6.RecipeStep[10].HeatCoolStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('HeatCoolEndedTime_10', 'Recipe_HeatCool_EndedTime_10', 'FieldPoint', 'R6.RecipeStep[10].HeatCoolEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('HeatCoolSetPoint_10', 'Recipe_HeatCool_SetPoint_10', 'FieldPoint', 'R6.RecipeStep[10].HeatCoolSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolDuration_10', 'Recipe_HeatCool_Duration_10', 'FieldPoint', 'R6.RecipeStep[10].HeatCoolDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('HeatCoolOperatingMode_10', 'Recipe_HeatCool_OperatingMode_10', 'FieldPoint', 'R6.RecipeStep[10].HeatCoolOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

/* Tags for Stirrer Block*/

	/* Tags for index 1 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_1', 'Recipe_Stirrer_Enabled_1', 'FieldPoint', 'R6.RecipeStep[1].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_1', 'Recipe_Stirrer_Started_1', 'FieldPoint', 'R6.RecipeStep[1].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_1', 'Recipe_Stirrer_Ended_1', 'FieldPoint', 'R6.RecipeStep[1].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_1', 'Recipe_Stirrer_StartedTime_1', 'FieldPoint', 'R6.RecipeStep[1].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_1', 'Recipe_Stirrer_EndedTime_1', 'FieldPoint', 'R6.RecipeStep[1].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_1', 'Recipe_Stirrer_SetPoint_1', 'FieldPoint', 'R6.RecipeStep[1].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 2 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_2', 'Recipe_Stirrer_Enabled_2', 'FieldPoint', 'R6.RecipeStep[2].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_2', 'Recipe_Stirrer_Started_2', 'FieldPoint', 'R6.RecipeStep[2].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_2', 'Recipe_Stirrer_Ended_2', 'FieldPoint', 'R6.RecipeStep[2].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_2', 'Recipe_Stirrer_StartedTime_2', 'FieldPoint', 'R6.RecipeStep[2].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_2', 'Recipe_Stirrer_EndedTime_2', 'FieldPoint', 'R6.RecipeStep[2].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_2', 'Recipe_Stirrer_SetPoint_2', 'FieldPoint', 'R6.RecipeStep[2].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 3 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_3', 'Recipe_Stirrer_Enabled_3', 'FieldPoint', 'R6.RecipeStep[3].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_3', 'Recipe_Stirrer_Started_3', 'FieldPoint', 'R6.RecipeStep[3].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_3', 'Recipe_Stirrer_Ended_3', 'FieldPoint', 'R6.RecipeStep[3].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_3', 'Recipe_Stirrer_StartedTime_3', 'FieldPoint', 'R6.RecipeStep[3].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_3', 'Recipe_Stirrer_EndedTime_3', 'FieldPoint', 'R6.RecipeStep[3].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_3', 'Recipe_Stirrer_SetPoint_3', 'FieldPoint', 'R6.RecipeStep[3].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 4 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_4', 'Recipe_Stirrer_Enabled_4', 'FieldPoint', 'R6.RecipeStep[4].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_4', 'Recipe_Stirrer_Started_4', 'FieldPoint', 'R6.RecipeStep[4].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_4', 'Recipe_Stirrer_Ended_4', 'FieldPoint', 'R6.RecipeStep[4].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_4', 'Recipe_Stirrer_StartedTime_4', 'FieldPoint', 'R6.RecipeStep[4].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_4', 'Recipe_Stirrer_EndedTime_4', 'FieldPoint', 'R6.RecipeStep[4].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_4', 'Recipe_Stirrer_SetPoint_4', 'FieldPoint', 'R6.RecipeStep[4].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 5 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_5', 'Recipe_Stirrer_Enabled_5', 'FieldPoint', 'R6.RecipeStep[5].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_5', 'Recipe_Stirrer_Started_5', 'FieldPoint', 'R6.RecipeStep[5].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_5', 'Recipe_Stirrer_Ended_5', 'FieldPoint', 'R6.RecipeStep[5].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_5', 'Recipe_Stirrer_StartedTime_5', 'FieldPoint', 'R6.RecipeStep[5].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_5', 'Recipe_Stirrer_EndedTime_5', 'FieldPoint', 'R6.RecipeStep[5].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_5', 'Recipe_Stirrer_SetPoint_5', 'FieldPoint', 'R6.RecipeStep[5].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 6 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_6', 'Recipe_Stirrer_Enabled_6', 'FieldPoint', 'R6.RecipeStep[6].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_6', 'Recipe_Stirrer_Started_6', 'FieldPoint', 'R6.RecipeStep[6].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_6', 'Recipe_Stirrer_Ended_6', 'FieldPoint', 'R6.RecipeStep[6].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_6', 'Recipe_Stirrer_StartedTime_6', 'FieldPoint', 'R6.RecipeStep[6].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_6', 'Recipe_Stirrer_EndedTime_6', 'FieldPoint', 'R6.RecipeStep[6].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_6', 'Recipe_Stirrer_SetPoint_6', 'FieldPoint', 'R6.RecipeStep[6].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 7 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_7', 'Recipe_Stirrer_Enabled_7', 'FieldPoint', 'R6.RecipeStep[7].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_7', 'Recipe_Stirrer_Started_7', 'FieldPoint', 'R6.RecipeStep[7].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_7', 'Recipe_Stirrer_Ended_7', 'FieldPoint', 'R6.RecipeStep[7].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_7', 'Recipe_Stirrer_StartedTime_7', 'FieldPoint', 'R6.RecipeStep[7].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_7', 'Recipe_Stirrer_EndedTime_7', 'FieldPoint', 'R6.RecipeStep[7].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_7', 'Recipe_Stirrer_SetPoint_7', 'FieldPoint', 'R6.RecipeStep[7].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 8 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_8', 'Recipe_Stirrer_Enabled_8', 'FieldPoint', 'R6.RecipeStep[8].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_8', 'Recipe_Stirrer_Started_8', 'FieldPoint', 'R6.RecipeStep[8].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_8', 'Recipe_Stirrer_Ended_8', 'FieldPoint', 'R6.RecipeStep[8].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_8', 'Recipe_Stirrer_StartedTime_8', 'FieldPoint', 'R6.RecipeStep[8].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_8', 'Recipe_Stirrer_EndedTime_8', 'FieldPoint', 'R6.RecipeStep[8].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_8', 'Recipe_Stirrer_SetPoint_8', 'FieldPoint', 'R6.RecipeStep[8].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 9 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_9', 'Recipe_Stirrer_Enabled_9', 'FieldPoint', 'R6.RecipeStep[9].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_9', 'Recipe_Stirrer_Started_9', 'FieldPoint', 'R6.RecipeStep[9].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_9', 'Recipe_Stirrer_Ended_9', 'FieldPoint', 'R6.RecipeStep[9].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_9', 'Recipe_Stirrer_StartedTime_9', 'FieldPoint', 'R6.RecipeStep[9].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_9', 'Recipe_Stirrer_EndedTime_9', 'FieldPoint', 'R6.RecipeStep[9].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_9', 'Recipe_Stirrer_SetPoint_9', 'FieldPoint', 'R6.RecipeStep[9].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 10 of array */
	insert into dbo.FieldPoints values('StirrerEnabled_10', 'Recipe_Stirrer_Enabled_10', 'FieldPoint', 'R6.RecipeStep[10].StirrerEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerStarted_10', 'Recipe_Stirrer_Started_10', 'FieldPoint', 'R6.RecipeStep[10].StirrerStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('StirrerEnded_10', 'Recipe_Stirrer_Ended_10', 'FieldPoint', 'R6.RecipeStep[10].StirrerEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('StirrerStartedTime_10', 'Recipe_Stirrer_StartedTime_10', 'FieldPoint', 'R6.RecipeStep[10].StirrerStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('StirrerEndedTime_10', 'Recipe_Stirrer_EndedTime_10', 'FieldPoint', 'R6.RecipeStep[10].StirrerEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('StirrerSetPoint_10', 'Recipe_Stirrer_SetPoint_10', 'FieldPoint', 'R6.RecipeStep[10].StirrerSetPointExpression', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

/* Tags for Wait Block */

	/* Tags for index 1 of array */
	insert into dbo.FieldPoints values('WaitEnabled_1', 'Recipe_Wait_Enabled_1', 'FieldPoint', 'R6.RecipeStep[1].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_1', 'Recipe_Wait_Started_1', 'FieldPoint', 'R6.RecipeStep[1].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_1', 'Recipe_Wait_Ended_1', 'FieldPoint', 'R6.RecipeStep[1].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_1', 'Recipe_Wait_StartedTime_1', 'FieldPoint', 'R6.RecipeStep[1].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_1', 'Recipe_Wait_EndedTime_1', 'FieldPoint', 'R6.RecipeStep[1].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_1', 'Recipe_Wait_Duration_1', 'FieldPoint', 'R6.RecipeStep[1].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_1', 'Recipe_Wait_RemainingTime_1', 'FieldPoint', 'R6.RecipeStep[1].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 2 of array */
	insert into dbo.FieldPoints values('WaitEnabled_2', 'Recipe_Wait_Enabled_2', 'FieldPoint', 'R6.RecipeStep[2].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
	insert into dbo.FieldPoints values('WaitStarted_2', 'Recipe_Wait_Started_2', 'FieldPoint', 'R6.RecipeStep[2].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_2', 'Recipe_Wait_Ended_2', 'FieldPoint', 'R6.RecipeStep[2].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_2', 'Recipe_Wait_StartedTime_2', 'FieldPoint', 'R6.RecipeStep[2].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_2', 'Recipe_Wait_EndedTime_2', 'FieldPoint', 'R6.RecipeStep[2].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_2', 'Recipe_Wait_Duration_2', 'FieldPoint', 'R6.RecipeStep[2].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_2', 'Recipe_Wait_RemainingTime_2', 'FieldPoint', 'R6.RecipeStep[2].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 3 of array */
	insert into dbo.FieldPoints values('WaitEnabled_3', 'Recipe_Wait_Enabled_3', 'FieldPoint', 'R6.RecipeStep[3].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_3', 'Recipe_Wait_Started_3', 'FieldPoint', 'R6.RecipeStep[3].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_3', 'Recipe_Wait_Ended_3', 'FieldPoint', 'R6.RecipeStep[3].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_3', 'Recipe_Wait_StartedTime_3', 'FieldPoint', 'R6.RecipeStep[3].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_3', 'Recipe_Wait_EndedTime_3', 'FieldPoint', 'R6.RecipeStep[3].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_3', 'Recipe_Wait_Duration_3', 'FieldPoint', 'R6.RecipeStep[3].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_3', 'Recipe_Wait_RemainingTime_3', 'FieldPoint', 'R6.RecipeStep[3].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 4 of array */
	insert into dbo.FieldPoints values('WaitEnabled_4', 'Recipe_Wait_Enabled_4', 'FieldPoint', 'R6.RecipeStep[4].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_4', 'Recipe_Wait_Started_4', 'FieldPoint', 'R6.RecipeStep[4].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_4', 'Recipe_Wait_Ended_4', 'FieldPoint', 'R6.RecipeStep[4].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_4', 'Recipe_Wait_StartedTime_4', 'FieldPoint', 'R6.RecipeStep[4].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_4', 'Recipe_Wait_EndedTime_4', 'FieldPoint', 'R6.RecipeStep[4].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_4', 'Recipe_Wait_Duration_4', 'FieldPoint', 'R6.RecipeStep[4].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_4', 'Recipe_Wait_RemainingTime_4', 'FieldPoint', 'R6.RecipeStep[4].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 5 of array */
	insert into dbo.FieldPoints values('WaitEnabled_5', 'Recipe_Wait_Enabled_5', 'FieldPoint', 'R6.RecipeStep[5].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_5', 'Recipe_Wait_Started_5', 'FieldPoint', 'R6.RecipeStep[5].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_5', 'Recipe_Wait_Ended_5', 'FieldPoint', 'R6.RecipeStep[5].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_5', 'Recipe_Wait_StartedTime_5', 'FieldPoint', 'R6.RecipeStep[5].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_5', 'Recipe_Wait_EndedTime_5', 'FieldPoint', 'R6.RecipeStep[5].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_5', 'Recipe_Wait_Duration_5', 'FieldPoint', 'R6.RecipeStep[5].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_5', 'Recipe_Wait_RemainingTime_5', 'FieldPoint', 'R6.RecipeStep[5].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 6 of array */
	insert into dbo.FieldPoints values('WaitEnabled_6', 'Recipe_Wait_Enabled_6', 'FieldPoint', 'R6.RecipeStep[6].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_6', 'Recipe_Wait_Started_6', 'FieldPoint', 'R6.RecipeStep[6].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_6', 'Recipe_Wait_Ended_6', 'FieldPoint', 'R6.RecipeStep[6].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_6', 'Recipe_Wait_StartedTime_6', 'FieldPoint', 'R6.RecipeStep[6].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_6', 'Recipe_Wait_EndedTime_6', 'FieldPoint', 'R6.RecipeStep[6].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_6', 'Recipe_Wait_Duration_6', 'FieldPoint', 'R6.RecipeStep[6].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_6', 'Recipe_Wait_RemainingTime_6', 'FieldPoint', 'R6.RecipeStep[6].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 7 of array */
	insert into dbo.FieldPoints values('WaitEnabled_7', 'Recipe_Wait_Enabled_7', 'FieldPoint', 'R6.RecipeStep[7].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_7', 'Recipe_Wait_Started_7', 'FieldPoint', 'R6.RecipeStep[7].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_7', 'Recipe_Wait_Ended_7', 'FieldPoint', 'R6.RecipeStep[7].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_7', 'Recipe_Wait_StartedTime_7', 'FieldPoint', 'R6.RecipeStep[7].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_7', 'Recipe_Wait_EndedTime_7', 'FieldPoint', 'R6.RecipeStep[7].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_7', 'Recipe_Wait_Duration_7', 'FieldPoint', 'R6.RecipeStep[7].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_7', 'Recipe_Wait_RemainingTime_7', 'FieldPoint', 'R6.RecipeStep[7].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 8 of array */
	insert into dbo.FieldPoints values('WaitEnabled_8', 'Recipe_Wait_Enabled_8', 'FieldPoint', 'R6.RecipeStep[8].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_8', 'Recipe_Wait_Started_8', 'FieldPoint', 'R6.RecipeStep[8].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_8', 'Recipe_Wait_Ended_8', 'FieldPoint', 'R6.RecipeStep[8].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_8', 'Recipe_Wait_StartedTime_8', 'FieldPoint', 'R6.RecipeStep[8].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_8', 'Recipe_Wait_EndedTime_8', 'FieldPoint', 'R6.RecipeStep[8].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_8', 'Recipe_Wait_Duration_8', 'FieldPoint', 'R6.RecipeStep[8].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_8', 'Recipe_Wait_RemainingTime_8', 'FieldPoint', 'R6.RecipeStep[8].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 9 of array */
	insert into dbo.FieldPoints values('WaitEnabled_9', 'Recipe_Wait_Enabled_9', 'FieldPoint', 'R6.RecipeStep[9].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_9', 'Recipe_Wait_Started_9', 'FieldPoint', 'R6.RecipeStep[9].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_9', 'Recipe_Wait_Ended_9', 'FieldPoint', 'R6.RecipeStep[9].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_9', 'Recipe_Wait_StartedTime_9', 'FieldPoint', 'R6.RecipeStep[9].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_9', 'Recipe_Wait_EndedTime_9', 'FieldPoint', 'R6.RecipeStep[9].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_9', 'Recipe_Wait_Duration_9', 'FieldPoint', 'R6.RecipeStep[9].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_9', 'Recipe_Wait_RemainingTime_9', 'FieldPoint', 'R6.RecipeStep[9].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/* Tags for index 10 of array */
	insert into dbo.FieldPoints values('WaitEnabled_10', 'Recipe_Wait_Enabled_10', 'FieldPoint', 'R6.RecipeStep[10].WaitEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitStarted_10', 'Recipe_Wait_Started_10', 'FieldPoint', 'R6.RecipeStep[10].WaitStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitEnded_10', 'Recipe_Wait_Ended_10', 'FieldPoint', 'R6.RecipeStep[10].WaitEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('WaitStartedTime_10', 'Recipe_Wait_StartedTime_10', 'FieldPoint', 'R6.RecipeStep[10].WaitStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('WaitEndedTime_10', 'Recipe_Wait_EndedTime_10', 'FieldPoint', 'R6.RecipeStep[10].WaitEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('WaitDuration_10', 'Recipe_Wait_Duration_10', 'FieldPoint', 'R6.RecipeStep[10].WaitDuration', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('WaitRemainingTime_10', 'Recipe_Wait_RemainingTime_10', 'FieldPoint', 'R6.RecipeStep[10].WaitRemainingTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

/* Tags for Dosing Block */

	/*Tags for index 1 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_1', 'Recipe_Dosing_Enabled_1', 'FieldPoint', 'R6.RecipeStep[1].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_1', 'Recipe_Dosing_Started_1', 'FieldPoint', 'R6.RecipeStep[1].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_1', 'Recipe_Dosing_Ended_1', 'FieldPoint', 'R6.RecipeStep[1].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_1', 'Recipe_Dosing_StartedTime_1', 'FieldPoint', 'R6.RecipeStep[1].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_1', 'Recipe_Dosing_EndedTime_1', 'FieldPoint', 'R6.RecipeStep[1].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_1', 'Recipe_Dosing_MaxAmount_1', 'FieldPoint', 'R6.RecipeStep[1].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_1', 'Recipe_Dosing_StopTemperature_1', 'FieldPoint', 'R6.RecipeStep[1].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_1', 'Recipe_Dosing_ResumeTemperature_1', 'FieldPoint', 'R6.RecipeStep[1].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_1', 'Recipe_Dosing_SetPoint_1', 'FieldPoint', 'R6.RecipeStep[1].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_1', 'Recipe_Dosing_MinRate_1', 'FieldPoint', 'R6.RecipeStep[1].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_1', 'Recipe_Dosing_MaxRate_1', 'FieldPoint', 'R6.RecipeStep[1].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_1', 'Recipe_Dosing_SettlingTime_1', 'FieldPoint', 'R6.RecipeStep[1].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_1', 'Recipe_Dosing_MinPh_1', 'FieldPoint', 'R6.RecipeStep[1].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_1', 'Recipe_Dosing_MaxPh_1', 'FieldPoint', 'R6.RecipeStep[1].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_1', 'Recipe_Dosing_OperatingMode_1', 'FieldPoint', 'R6.RecipeStep[1].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 2 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_2', 'Recipe_Dosing_Enabled_2', 'FieldPoint', 'R6.RecipeStep[2].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_2', 'Recipe_Dosing_Started_2', 'FieldPoint', 'R6.RecipeStep[2].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_2', 'Recipe_Dosing_Ended_2', 'FieldPoint', 'R6.RecipeStep[2].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_2', 'Recipe_Dosing_StartedTime_2', 'FieldPoint', 'R6.RecipeStep[2].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_2', 'Recipe_Dosing_EndedTime_2', 'FieldPoint', 'R6.RecipeStep[2].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_2', 'Recipe_Dosing_MaxAmount_2', 'FieldPoint', 'R6.RecipeStep[2].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_2', 'Recipe_Dosing_StopTemperature_2', 'FieldPoint', 'R6.RecipeStep[2].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_2', 'Recipe_Dosing_ResumeTemperature_2', 'FieldPoint', 'R6.RecipeStep[2].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_2', 'Recipe_Dosing_SetPoint_2', 'FieldPoint', 'R6.RecipeStep[2].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_2', 'Recipe_Dosing_MinRate_2', 'FieldPoint', 'R6.RecipeStep[2].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_2', 'Recipe_Dosing_MaxRate_2', 'FieldPoint', 'R6.RecipeStep[2].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_2', 'Recipe_Dosing_SettlingTime_2', 'FieldPoint', 'R6.RecipeStep[2].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_2', 'Recipe_Dosing_MinPh_2', 'FieldPoint', 'R6.RecipeStep[2].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_2', 'Recipe_Dosing_MaxPh_2', 'FieldPoint', 'R6.RecipeStep[2].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_2', 'Recipe_Dosing_OperatingMode_2', 'FieldPoint', 'R6.RecipeStep[2].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 3 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_3', 'Recipe_Dosing_Enabled_3', 'FieldPoint', 'R6.RecipeStep[3].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_3', 'Recipe_Dosing_Started_3', 'FieldPoint', 'R6.RecipeStep[3].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_3', 'Recipe_Dosing_Ended_3', 'FieldPoint', 'R6.RecipeStep[3].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_3', 'Recipe_Dosing_StartedTime_3', 'FieldPoint', 'R6.RecipeStep[3].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_3', 'Recipe_Dosing_EndedTime_3', 'FieldPoint', 'R6.RecipeStep[3].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_3', 'Recipe_Dosing_MaxAmount_3', 'FieldPoint', 'R6.RecipeStep[3].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_3', 'Recipe_Dosing_StopTemperature_3', 'FieldPoint', 'R6.RecipeStep[3].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_3', 'Recipe_Dosing_ResumeTemperature_3', 'FieldPoint', 'R6.RecipeStep[3].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_3', 'Recipe_Dosing_SetPoint_3', 'FieldPoint', 'R6.RecipeStep[3].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_3', 'Recipe_Dosing_MinRate_3', 'FieldPoint', 'R6.RecipeStep[3].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_3', 'Recipe_Dosing_MaxRate_3', 'FieldPoint', 'R6.RecipeStep[3].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_3', 'Recipe_Dosing_SettlingTime_3', 'FieldPoint', 'R6.RecipeStep[3].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_3', 'Recipe_Dosing_MinPh_3', 'FieldPoint', 'R6.RecipeStep[3].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_3', 'Recipe_Dosing_MaxPh_3', 'FieldPoint', 'R6.RecipeStep[3].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_3', 'Recipe_Dosing_OperatingMode_3', 'FieldPoint', 'R6.RecipeStep[3].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 4 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_4', 'Recipe_Dosing_Enabled_4', 'FieldPoint', 'R6.RecipeStep[4].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_4', 'Recipe_Dosing_Started_4', 'FieldPoint', 'R6.RecipeStep[4].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_4', 'Recipe_Dosing_Ended_4', 'FieldPoint', 'R6.RecipeStep[4].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_4', 'Recipe_Dosing_StartedTime_4', 'FieldPoint', 'R6.RecipeStep[4].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_4', 'Recipe_Dosing_EndedTime_4', 'FieldPoint', 'R6.RecipeStep[4].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_4', 'Recipe_Dosing_MaxAmount_4', 'FieldPoint', 'R6.RecipeStep[4].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_4', 'Recipe_Dosing_StopTemperature_4', 'FieldPoint', 'R6.RecipeStep[4].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_4', 'Recipe_Dosing_ResumeTemperature_4', 'FieldPoint', 'R6.RecipeStep[4].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_4', 'Recipe_Dosing_SetPoint_4', 'FieldPoint', 'R6.RecipeStep[4].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_4', 'Recipe_Dosing_MinRate_4', 'FieldPoint', 'R6.RecipeStep[4].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_4', 'Recipe_Dosing_MaxRate_4', 'FieldPoint', 'R6.RecipeStep[4].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_4', 'Recipe_Dosing_SettlingTime_4', 'FieldPoint', 'R6.RecipeStep[4].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_4', 'Recipe_Dosing_MinPh_4', 'FieldPoint', 'R6.RecipeStep[4].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_4', 'Recipe_Dosing_MaxPh_4', 'FieldPoint', 'R6.RecipeStep[4].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_4', 'Recipe_Dosing_OperatingMode_4', 'FieldPoint', 'R6.RecipeStep[4].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 5 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_5', 'Recipe_Dosing_Enabled_5', 'FieldPoint', 'R6.RecipeStep[5].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_5', 'Recipe_Dosing_Started_5', 'FieldPoint', 'R6.RecipeStep[5].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_5', 'Recipe_Dosing_Ended_5', 'FieldPoint', 'R6.RecipeStep[5].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_5', 'Recipe_Dosing_StartedTime_5', 'FieldPoint', 'R6.RecipeStep[5].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_5', 'Recipe_Dosing_EndedTime_5', 'FieldPoint', 'R6.RecipeStep[5].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_5', 'Recipe_Dosing_MaxAmount_5', 'FieldPoint', 'R6.RecipeStep[5].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_5', 'Recipe_Dosing_StopTemperature_5', 'FieldPoint', 'R6.RecipeStep[5].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_5', 'Recipe_Dosing_ResumeTemperature_5', 'FieldPoint', 'R6.RecipeStep[5].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_5', 'Recipe_Dosing_SetPoint_5', 'FieldPoint', 'R6.RecipeStep[5].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_5', 'Recipe_Dosing_MinRate_5', 'FieldPoint', 'R6.RecipeStep[5].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_5', 'Recipe_Dosing_MaxRate_5', 'FieldPoint', 'R6.RecipeStep[5].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_5', 'Recipe_Dosing_SettlingTime_5', 'FieldPoint', 'R6.RecipeStep[5].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_5', 'Recipe_Dosing_MinPh_5', 'FieldPoint', 'R6.RecipeStep[5].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_5', 'Recipe_Dosing_MaxPh_5', 'FieldPoint', 'R6.RecipeStep[5].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_5', 'Recipe_Dosing_OperatingMode_5', 'FieldPoint', 'R6.RecipeStep[5].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 6 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_6', 'Recipe_Dosing_Enabled_6', 'FieldPoint', 'R6.RecipeStep[6].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_6', 'Recipe_Dosing_Started_6', 'FieldPoint', 'R6.RecipeStep[6].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_6', 'Recipe_Dosing_Ended_6', 'FieldPoint', 'R6.RecipeStep[6].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_6', 'Recipe_Dosing_StartedTime_6', 'FieldPoint', 'R6.RecipeStep[6].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_6', 'Recipe_Dosing_EndedTime_6', 'FieldPoint', 'R6.RecipeStep[6].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_6', 'Recipe_Dosing_MaxAmount_6', 'FieldPoint', 'R6.RecipeStep[6].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_6', 'Recipe_Dosing_StopTemperature_6', 'FieldPoint', 'R6.RecipeStep[6].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_6', 'Recipe_Dosing_ResumeTemperature_6', 'FieldPoint', 'R6.RecipeStep[6].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_6', 'Recipe_Dosing_SetPoint_6', 'FieldPoint', 'R6.RecipeStep[6].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_6', 'Recipe_Dosing_MinRate_6', 'FieldPoint', 'R6.RecipeStep[6].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_6', 'Recipe_Dosing_MaxRate_6', 'FieldPoint', 'R6.RecipeStep[6].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_6', 'Recipe_Dosing_SettlingTime_6', 'FieldPoint', 'R6.RecipeStep[6].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_6', 'Recipe_Dosing_MinPh_6', 'FieldPoint', 'R6.RecipeStep[6].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_6', 'Recipe_Dosing_MaxPh_6', 'FieldPoint', 'R6.RecipeStep[6].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_6', 'Recipe_Dosing_OperatingMode_6', 'FieldPoint', 'R6.RecipeStep[6].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 7 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_7', 'Recipe_Dosing_Enabled_7', 'FieldPoint', 'R6.RecipeStep[7].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_7', 'Recipe_Dosing_Started_7', 'FieldPoint', 'R6.RecipeStep[7].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_7', 'Recipe_Dosing_Ended_7', 'FieldPoint', 'R6.RecipeStep[7].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_7', 'Recipe_Dosing_StartedTime_7', 'FieldPoint', 'R6.RecipeStep[7].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_7', 'Recipe_Dosing_EndedTime_7', 'FieldPoint', 'R6.RecipeStep[7].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_7', 'Recipe_Dosing_MaxAmount_7', 'FieldPoint', 'R6.RecipeStep[7].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_7', 'Recipe_Dosing_StopTemperature_7', 'FieldPoint', 'R6.RecipeStep[7].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_7', 'Recipe_Dosing_ResumeTemperature_7', 'FieldPoint', 'R6.RecipeStep[7].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_7', 'Recipe_Dosing_SetPoint_7', 'FieldPoint', 'R6.RecipeStep[7].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_7', 'Recipe_Dosing_MinRate_7', 'FieldPoint', 'R6.RecipeStep[7].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_7', 'Recipe_Dosing_MaxRate_7', 'FieldPoint', 'R6.RecipeStep[7].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_7', 'Recipe_Dosing_SettlingTime_7', 'FieldPoint', 'R6.RecipeStep[7].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_7', 'Recipe_Dosing_MinPh_7', 'FieldPoint', 'R6.RecipeStep[7].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_7', 'Recipe_Dosing_MaxPh_7', 'FieldPoint', 'R6.RecipeStep[7].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_7', 'Recipe_Dosing_OperatingMode_7', 'FieldPoint', 'R6.RecipeStep[7].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 8 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_8', 'Recipe_Dosing_Enabled_8', 'FieldPoint', 'R6.RecipeStep[8].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_8', 'Recipe_Dosing_Started_8', 'FieldPoint', 'R6.RecipeStep[8].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_8', 'Recipe_Dosing_Ended_8', 'FieldPoint', 'R6.RecipeStep[8].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_8', 'Recipe_Dosing_StartedTime_8', 'FieldPoint', 'R6.RecipeStep[8].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_8', 'Recipe_Dosing_EndedTime_8', 'FieldPoint', 'R6.RecipeStep[8].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_8', 'Recipe_Dosing_MaxAmount_8', 'FieldPoint', 'R6.RecipeStep[8].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_8', 'Recipe_Dosing_StopTemperature_8', 'FieldPoint', 'R6.RecipeStep[8].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_8', 'Recipe_Dosing_ResumeTemperature_8', 'FieldPoint', 'R6.RecipeStep[8].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_8', 'Recipe_Dosing_SetPoint_8', 'FieldPoint', 'R6.RecipeStep[8].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_8', 'Recipe_Dosing_MinRate_8', 'FieldPoint', 'R6.RecipeStep[8].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_8', 'Recipe_Dosing_MaxRate_8', 'FieldPoint', 'R6.RecipeStep[8].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_8', 'Recipe_Dosing_SettlingTime_8', 'FieldPoint', 'R6.RecipeStep[8].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_8', 'Recipe_Dosing_MinPh_8', 'FieldPoint', 'R6.RecipeStep[8].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_8', 'Recipe_Dosing_MaxPh_8', 'FieldPoint', 'R6.RecipeStep[8].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_8', 'Recipe_Dosing_OperatingMode_8', 'FieldPoint', 'R6.RecipeStep[8].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 9 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_9', 'Recipe_Dosing_Enabled_9', 'FieldPoint', 'R6.RecipeStep[9].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_9', 'Recipe_Dosing_Started_9', 'FieldPoint', 'R6.RecipeStep[9].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_9', 'Recipe_Dosing_Ended_9', 'FieldPoint', 'R6.RecipeStep[9].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_9', 'Recipe_Dosing_StartedTime_9', 'FieldPoint', 'R6.RecipeStep[9].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_9', 'Recipe_Dosing_EndedTime_9', 'FieldPoint', 'R6.RecipeStep[9].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_9', 'Recipe_Dosing_MaxAmount_9', 'FieldPoint', 'R6.RecipeStep[9].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_9', 'Recipe_Dosing_StopTemperature_9', 'FieldPoint', 'R6.RecipeStep[9].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_9', 'Recipe_Dosing_ResumeTemperature_9', 'FieldPoint', 'R6.RecipeStep[9].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_9', 'Recipe_Dosing_SetPoint_9', 'FieldPoint', 'R6.RecipeStep[9].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_9', 'Recipe_Dosing_MinRate_9', 'FieldPoint', 'R6.RecipeStep[9].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_9', 'Recipe_Dosing_MaxRate_9', 'FieldPoint', 'R6.RecipeStep[9].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_9', 'Recipe_Dosing_SettlingTime_9', 'FieldPoint', 'R6.RecipeStep[9].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_9', 'Recipe_Dosing_MinPh_9', 'FieldPoint', 'R6.RecipeStep[9].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_9', 'Recipe_Dosing_MaxPh_9', 'FieldPoint', 'R6.RecipeStep[9].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_9', 'Recipe_Dosing_OperatingMode_9', 'FieldPoint', 'R6.RecipeStep[9].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	/*Tags for index 10 of array*/
	insert into dbo.FieldPoints values('DosingEnabled_10', 'Recipe_Dosing_Enabled_10', 'FieldPoint', 'R6.RecipeStep[10].DosingEnabled', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingStarted_10', 'Recipe_Dosing_Started_10', 'FieldPoint', 'R6.RecipeStep[10].DosingStarted', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingEnded_10', 'Recipe_Dosing_Ended_10', 'FieldPoint', 'R6.RecipeStep[10].DosingEnded', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	
insert into dbo.FieldPoints values('DosingStartedTime_10', 'Recipe_Dosing_StartedTime_10', 'FieldPoint', 'R6.RecipeStep[10].DosingStartedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
insert into dbo.FieldPoints values('DosingEndedTime_10', 'Recipe_Dosing_EndedTime_10', 'FieldPoint', 'R6.RecipeStep[10].DosingEndedTime', 'string', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')

	insert into dbo.FieldPoints values('DosingMaxAmount_10', 'Recipe_Dosing_MaxAmount_10', 'FieldPoint', 'R6.RecipeStep[10].DosingMaxAmount', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingStopTemperature_10', 'Recipe_Dosing_StopTemperature_10', 'FieldPoint', 'R6.RecipeStep[10].DosingStopTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingResumeTemperature_10', 'Recipe_Dosing_ResumeTemperature_10', 'FieldPoint', 'R6.RecipeStep[10].DosingResumeTemp', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSetPoint_10', 'Recipe_Dosing_SetPoint_10', 'FieldPoint', 'R6.RecipeStep[10].DosingRateSetPointExpression', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinRate_10', 'Recipe_Dosing_MinRate_10', 'FieldPoint', 'R6.RecipeStep[10].DosingMinRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxRate_10', 'Recipe_Dosing_MaxRate_10', 'FieldPoint', 'R6.RecipeStep[10].DosingMaxRate', 'float', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingSettlingTime_10', 'Recipe_Dosing_SettlingTime_10', 'FieldPoint', 'R6.RecipeStep[10].DosingSettlingTime', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMinPh_10', 'Recipe_Dosing_MinPh_10', 'FieldPoint', 'R6.RecipeStep[10].DosingMinPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingMaxPh_10', 'Recipe_Dosing_MaxPh_10', 'FieldPoint', 'R6.RecipeStep[10].DosingMaxPh', 'int', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')
	insert into dbo.FieldPoints values('DosingOperatingMode_10', 'Recipe_Dosing_OperatingMode_10', 'FieldPoint', 'R6.RecipeStep[10].DosingOperatingMode', 'bool', 'false', 'false', 'sensorDataSet_6', 'Reactor_6')