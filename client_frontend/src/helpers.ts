import { useRef, useEffect } from 'react';
import type { MutableRefObject } from 'react';

/**
 * a type-safe version of the `usePrevious` hook described here:
 * @see {@link https://reactjs.org/docs/hooks-faq.html#how-to-get-the-previous-props-or-state}
 */
export function usePrevious<T>(
  value: T,
): MutableRefObject<T | undefined>['current'] {
  const ref = useRef<T>();
  useEffect(() => {
    ref.current = value;
  }, [value]);
  return ref.current;
}

export function keyTitle (key: string, regex?: RegExp) {
  const regexToUse = regex || /[a-z]+|[A-Z][a-z]+/g;
  const lowerCasedKey = key.match(regexToUse)
    ?.join(' ')
    ?.toLowerCase();
  const headerName = lowerCasedKey?.replace(lowerCasedKey[0], lowerCasedKey[0].toUpperCase());
  return headerName;
}