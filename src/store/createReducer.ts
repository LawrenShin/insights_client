import {RequestStatuses} from "../api/requestTypes";
import {AnyAction} from "redux";

export interface State {
  data: any;
  status: RequestStatuses;
  error: string | null;
}

export default function (name: string) {
  const initState = {
    data: null,
    status: RequestStatuses.still,
    error: null,
  };

  return function (state: State = initState, action: AnyAction) {
    const {type, payload} = action;

    if (type === `${name}_FAIL`) return {
      ...state,
      error: payload,
    }

    if (type === `${name}_LOAD`) return {
      ...state,
      error: null,
      status: RequestStatuses.loading,
    }

    if (type === `${name}_SUCCESS`) return {
      ...state,
      error: null,
      status: RequestStatuses.still,
      data: payload,
    }

    return state;
  }
}