Neo.Plugins.OracleService.Filter("[[[[[[{}]]]]]]", "$"+string.Concat(Enumerable.Repeat("[0"+string.Concat(Enumerable.Repeat(",0",64)) +"]", 6)));

// This will also cause the same problem.
// Neo.Plugins.OracleService.Filter("{\"a\":{\"a\":{\"a\":{\"a\":{\"a\":{\"a\":{\"a\":{}}}}}}}}", "$['a','a']['a','a']['a','a']['a','a']['a','a']['a','a']");
