import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './common/components/home/home.component';
import { canActivateAuthenticatedRouteGuard, canActivateChildAuthenticatedRouteGuard, unsavedChangesGuard } from './common/guards/guards';
import { LoginComponent } from './modules/users/components/login/login.component';
import { SignupComponent } from './modules/users/components/signup/signup.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'app',
  },
  {
    path: 'app',
    canActivate: [canActivateAuthenticatedRouteGuard],
    canActivateChild: [canActivateChildAuthenticatedRouteGuard],
    canDeactivate: [unsavedChangesGuard],
    children: [
      {
        path: '',
        component: HomeComponent,
        data:{animation: 'homePage'}
      },
      {
        path: 'users',
        loadChildren: () =>
          import('./modules/users/users.module').then((m) => m.UsersModule),
      },
      {
        path: 'sessions',
        loadChildren: () =>
          import('./modules/sessions/sessions.module').then(
            (m) => m.SessionsModule
          ),
      },
      {
        path: 'movies',
        loadChildren: () =>
          import('./modules/movies/movies.module').then((m) => m.MoviesModule),
      },
      {
        path: 'rooms',
        loadChildren: () =>
          import('./modules/rooms/rooms.module').then((m) => m.RoomsModule),
      },
      {
        path: 'genres',
        loadChildren: () =>
          import('./modules/genres/genres.module').then((m) => m.GenresModule),
      },
    ],
  },
  {
    path: 'login',
    component: LoginComponent,
    data:{animation: 'loginPage'}
  },
  {
    path: 'signup',
    component: SignupComponent,
    data:{animation: 'signUpPage'}
  },
  {
    path: '**',
    redirectTo: 'app'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
