import React from 'react';
import { useField } from 'formik';
import { TextField } from '@material-ui/core';

const Input = (props: any) => {
  const { errorText, ...rest } = props;
  const [field, meta] = useField(props);

  function _renderHelperText() {
    const {touched, error} = meta;
    if (touched && error) {
      return error;
    }
  }

  return (
    <TextField
      type="text"
      error={!!(meta.touched && meta.error)}
      helperText={_renderHelperText()}
      {...field}
      {...rest}
    />
  );
}

export default Input;
