import { animate, animateChild, group, query, style, transition, trigger } from "@angular/animations";

const defaultSlideLeftAnimations = [
  style({ position: 'relative' }),
  query(':enter, :leave', [
    style({
      position: 'absolute',
      top: 0,
      right: 0,
      width: '100%'
    })
  ]),
  query(':enter', [
    style({ right: '-100%' })
  ]),
  query(':leave', animateChild()),
  group([
    query(':leave', [
      animate('350ms ease-out', style({ right: '100%', opacity: 0 }))
    ]),
    query(':enter', [
      animate('350ms ease-out', style({ right: '0%' }))
    ]),
    query('@*', animateChild(), {optional: true})
  ]),
];

export const showStateTrigger = trigger('show',[
    transition(':enter',[
        style({
            opacity:0
        }),
        animate(350, style({
            opacity: 1,
        }))
    ]),
    transition(':leave',[
        animate(350, style({
            opacity: 0,
        }))
    ])
]);

export const slideInAnimation =
  trigger('routeAnimations', [
    transition('* <=> *', [
      style({ position: 'relative' }),
      query(':enter, :leave', [
        style({
          position: 'absolute',
          top: 0,
          left: 0,
          width: '100%'
        })
      ], { optional: true }),
      query(':enter', [
        style({ left: '-100%' })
      ], { optional: true }),
      query(':leave', animateChild(), { optional: true }),
      group([
        query(':leave', [
          animate('200ms ease-out', style({ left: '100%', opacity: 0 }))
        ], { optional: true }),
        query(':enter', [
          animate('300ms ease-out', style({ left: '0%' }))
        ], { optional: true }),
        query('@*', animateChild(), { optional: true })
      ]),
    ])
  ]);
