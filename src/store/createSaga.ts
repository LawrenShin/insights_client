import {call, put, takeLatest} from "typed-redux-saga";
import {getRequest} from "../api";
import {CreateAction} from "./actionType";
import {SignInType} from "../pages/SignIn/duck";


export default function (name: string) {

  function* worker (action: any) {
    const {url, params} = action.payload;

    try {
      const res = yield call(getRequest, url, params);
      yield put(CreateAction(`${name}_SUCCESS`, res));
    } catch(error: any) {
      if (error.message === '403') return yield put(CreateAction(SignInType.LOGOUT));
      yield put(CreateAction(`${name}_FAIL`, error.message));
    }
  }

  return function* watcher() {
    yield takeLatest(`${name}_LOAD`, worker);
  }
}
