﻿import { Component, Input } from '@angular/core';
import {NG_VALIDATORS } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import { ViewModelFactory } from "./view-model-factory.service";
import { UrlManager } from "./urlmanager.service";
import * as Models from "./models";
import * as ViewModels from "./nakedobjects.viewmodels";
import { AbstractDroppableComponent } from './abstract-droppable.component';

@Component({
    selector: 'parameter',
    templateUrl: 'app/parameter.component.html',
    styleUrls: ['app/parameter.component.css']
})
export class ParameterComponent extends AbstractDroppableComponent {

    constructor(private viewModelFactory: ViewModelFactory, private urlManager: UrlManager) {
        super();
    }

    parm: ViewModels.ParameterViewModel;

    @Input()
    parent: ViewModels.DialogViewModel;

    @Input()
    set parameter(value: ViewModels.ParameterViewModel) {
        this.parm = value;
        this.droppable = value as ViewModels.IFieldViewModel;
    }

    get parameter() {
        return this.parm;
    }

   
    classes(): string {
        return `${this.parm.color}${this.canDrop ? " candrop" : ""}`;
    }

}