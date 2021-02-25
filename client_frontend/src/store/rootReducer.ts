import {combineReducers} from "redux";
import {reducer as SignIn} from '../pages/SignIn/duck';

const rootReducer = combineReducers({
  SignIn,
});

export type RootState = ReturnType<typeof rootReducer>

export default rootReducer;
