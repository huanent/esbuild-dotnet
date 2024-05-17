#load "common.csx"
using static Common;

var key = Args.FirstOrDefault(f => !string.IsNullOrWhiteSpace(f));
PublishPackage("Esbuild", key);

