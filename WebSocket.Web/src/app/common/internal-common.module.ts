import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialExportModule } from '../imported-modules/material.module';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';



@NgModule({
  declarations: [LoadingSpinnerComponent],
  imports: [
    CommonModule,
    MaterialExportModule
  ],
  exports: [LoadingSpinnerComponent]
})
export class InternalCommonModule { }
