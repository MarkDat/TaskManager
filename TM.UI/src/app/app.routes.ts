import { Route } from "@angular/router";

export const appRoutes: Route[] = [
    {
      path: '',
      redirectTo: '/index',
      pathMatch: 'full',
    },
    {
      path: 'projects',
      loadChildren: () => import('./modules/project/project.module').then(m => m.ProjectModule)
    }
];