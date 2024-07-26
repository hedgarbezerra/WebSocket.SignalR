import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { UsersComponent } from './users.component';
import { MomentModule } from 'ngx-moment';
import { MaterialExportModule } from '../../imported-modules/material.module';


@NgModule({
  declarations: [
    UsersComponent,
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    MomentModule,
    MaterialExportModule
  ],
  exports: [UsersRoutingModule]
})
export class UsersModule { }
