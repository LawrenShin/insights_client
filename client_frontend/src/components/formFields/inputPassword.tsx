import {FormControl, IconButton, InputAdornment, InputLabel, OutlinedInput, FormHelperText} from "@material-ui/core";
import {Visibility, VisibilityOff} from "@material-ui/icons";
import React from "react";
import {useField} from "formik";
import { at } from 'lodash';


export default ({
  variant,
  size,
  ...props
  }: any) => {
  const [showPassword, setShowPassword] = React.useState<boolean>(false);
  const handleClickShowPassword = () => setShowPassword(!showPassword);
  const handleMouseDownPassword = (event: React.MouseEvent<HTMLButtonElement>) => {
    event.preventDefault();
  };

  const { errorText, ...rest } = props;
  const [field, meta] = useField(props);
  const [touched, error] = at(meta, 'touched', 'error');
  // use as boolean otherwise u get warning
  const showError = !!(touched && error?.password);

  function _renderHelperText() {
    if (showError) {
      return <FormHelperText
        error={showError}
      >{error?.password}</FormHelperText>
    }
    return<></>
  }

  return (
    <FormControl
      variant={variant}
      size={size}
    >
      <InputLabel
        error={showError}
        htmlFor="password"
      >
        Password
      </InputLabel>
      <OutlinedInput
        {...field}
        {...rest}
        error={showError}
        name={'password'}
        type={showPassword ? 'text' : 'password'}
        endAdornment={
          <InputAdornment position="end">
            <IconButton
              aria-label="toggle password visibility"
              onClick={handleClickShowPassword}
              onMouseDown={handleMouseDownPassword}
              edge="end"
            >
              {showPassword ? <Visibility/> : <VisibilityOff/>}
            </IconButton>
          </InputAdornment>
        }
        labelWidth={70}
      />
      {_renderHelperText()}
    </FormControl>
  )
}