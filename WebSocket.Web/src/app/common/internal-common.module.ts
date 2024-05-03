import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { MaterialExportModule } from '../imported-modules/material.module';



@NgModule({
  declarations: [LoadingSpinnerComponent],
  imports: [
    CommonModule,
    MaterialExportModule
  ],
  exports: [ LoadingSpinnerComponent]
})
export class InternalCommonModule { }
