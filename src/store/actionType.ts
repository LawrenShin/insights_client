export interface Action<T, P> {
  type: T;
  payload?: P;
}

export function CreateAction<T extends string, P>(type: T, payload?: P): Action<T, P> {
  return { type, payload };
}
