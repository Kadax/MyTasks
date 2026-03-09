import { HttpInterceptorFn, HttpRequest, HttpHandlerFn } from '@angular/common/http';

export const credentialsInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
  // Clone the request and set withCredentials to true
  const modifiedReq = req.clone({
    withCredentials: true
  });
  return next(modifiedReq);
};
