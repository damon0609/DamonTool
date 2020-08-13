using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Damon.MVC;
public class CSVController : BaseController
{
    private CSVModel mModel;
    public CSVController(IModel model) : base(model)
    {
        mModel = model as CSVModel;
    }

    public List<CSVModel.CSVInfo> csvList {get{return mModel.csvList;}}
}

