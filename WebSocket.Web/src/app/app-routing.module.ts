import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'app',
  },
  {
    path: 'app',
    //canActivate: [guards.canActivateAuthenticated],
    //canActivateChild: [guards.canActivateAuthenticatedChild],
    children: [
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
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
